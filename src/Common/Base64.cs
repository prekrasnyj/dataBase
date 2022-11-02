using System.Text;

namespace Common;

public static class Base64
{
    const int _MaxCharsInLine = 76;
    const int _MaxBytesInLine = _MaxCharsInLine / 4 * 3;

    public static byte[] Decode(char[] text)
    {
        return Convert.FromBase64CharArray(text, 0, text.Length);
    }

    public static byte[] Decode(char[] text, int offset, int length)
    {
        return Convert.FromBase64CharArray(text, offset, length);
    }

    public static byte[] Decode(string text)
    {
        return Convert.FromBase64String(text);
    }

    public static void Decode(char[] text, Stream binaryOut)
    {
        Decode(text, 0, text.Length, binaryOut);
    }

    public static void Decode(char[] text, int offset, int length, Stream binaryOut)
    {
        const int portionSize = 64 * 1024; // размер кратен 4
        if (length < portionSize * 2)
        {
            // нет смысла выпендриваться
            byte[] data = Convert.FromBase64CharArray(text, offset, length);
            binaryOut.Write(data, 0, data.Length);
            return;
        }
        int index = offset; // первый необработанный символ в строке
        int chars = 0;      // накоплено значащих символов
        int limit = offset + length;
        for (int i = offset; i < limit; i++)
        {
            if (text[i] > '\x20')
            {
                if (++chars == portionSize)
                {
                    // накопили достаточно символов
                    byte[] data = Convert.FromBase64CharArray(text, index, i + 1 - index);
                    binaryOut.Write(data, 0, data.Length);
                    index = i + 1;
                    chars = 0;
                }
            }
        }
        if (chars > 0)
        {
            // декодируем остатки
            byte[] data = Convert.FromBase64CharArray(text, index, limit - index);
            binaryOut.Write(data, 0, data.Length);
        }
    }

    public static void Decode(string text, Stream binaryOut)
    {
        const int bufferSize = 64 * 1024; // размер кратен 4; чем больше, тем быстрее
        int textLen = text.Length;
        if (textLen < bufferSize * 2)
        {
            // нет смысла выпендриваться
            byte[] data = Convert.FromBase64String(text);
            binaryOut.Write(data, 0, data.Length);
            return;
        }
        char[] buffer = new char[bufferSize];
        int len = 0;   // заполнение буфера
        int index = 0; // первый непробельный символ в строке, который еще не попал в буфер
        for (int i = 0; i < textLen; i++)
        {
            if (text[i] <= '\x20') // char.IsWhiteSpace работает медленнее!
            {
                // встретили пробельный символ
                // отправляем в буфер накопленную непробельную строку (если i > index)
                while (i - index >= bufferSize - len)
                {
                    // если буфер заполнен, то декодируем его и отправляем данные в binaryOut
                    text.CopyTo(index, buffer, len, bufferSize - len);
                    index += bufferSize - len;
                    byte[] data = Convert.FromBase64CharArray(buffer, 0, bufferSize);
                    binaryOut.Write(data, 0, data.Length);
                    len = 0;
                }
                if (i > index)
                {
                    text.CopyTo(index, buffer, len, i - index);
                    len += i - index;
                }
                // index будет обновляться до тех пор, пока мы идем по пробельным символам
                index = i + 1;
            }
        }
        while (textLen - index >= bufferSize - len)
        {
            // если буфер заполнен, то декодируем его и отправляем данные в binaryOut
            text.CopyTo(index, buffer, len, bufferSize - len);
            index += bufferSize - len;
            byte[] data = Convert.FromBase64CharArray(buffer, 0, bufferSize);
            binaryOut.Write(data, 0, data.Length);
            len = 0;
        }
        if (index < textLen)
        {
            // помещаем в буфер остаток строки
            text.CopyTo(index, buffer, len, textLen - index);
            len += textLen - index;
        }
        if (len > 0)
        {
            // декодируем последнюю порцию данных
            byte[] data = Convert.FromBase64CharArray(buffer, 0, len);
            binaryOut.Write(data, 0, data.Length);
        }
    }

    public static void Decode(TextReader reader, Stream binaryOut)
    {
        /*byte[] data = Convert.FromBase64String(reader.ReadToEnd());
        binaryOut.Write(data, 0, data.Length);*/
        const int bufferSize = 64 * 1024;
        char[] buffer = new char[bufferSize];
        int len = 0;
        while (true)
        {
            byte[] data;
            while (len < bufferSize)
            {
                int cnt = reader.Read(buffer, len, bufferSize - len);
                len += cnt;
                if (cnt == 0) // end of stream
                {
                    data = Convert.FromBase64CharArray(buffer, 0, len);
                    binaryOut.Write(data, 0, data.Length);
                    return; // а все!
                }
            }
            // у нас полностью заполненный буфер
            int chars = 0;
            len = 0;
            for (int i = 0; i < bufferSize; i++)
            {
                if (buffer[i] > '\x20')
                {
                    if (++chars == 4)
                    {
                        // запоминаем часть буфера, которая содержит полные 4-ки символов
                        len = i + 1;
                        chars = 0;
                    }
                }
            }
            // конвертируем только полные 4-ки символов
            data = Convert.FromBase64CharArray(buffer, 0, len);
            binaryOut.Write(data, 0, data.Length);
            if (chars > 0)
            {
                // в буфере остались значимые символы
                Array.Copy(buffer, len, buffer, 0, bufferSize - len);
                len = bufferSize - len;
            }
            else
                len = 0; // выкидываем оставшиеся пробелы
        }
    }

    public static void Decode(Stream textIn, Encoding encoding, Stream binaryOut)
    {
        using (var reader = new StreamReader(textIn, encoding, false, 4 * 1024, true))
            Decode(reader, binaryOut);
    }

    static void Encode(byte[] bytes, int index, int length, Action<char[], int, int> putLine)
    {
        const int _LineCnt = 50; // оптимальное значение 
        char[] line = new char[_MaxCharsInLine * _LineCnt];
        while (length >= _MaxBytesInLine * _LineCnt)
        {
            Convert.ToBase64CharArray(bytes, index, _MaxBytesInLine * _LineCnt, line, 0);
            length -= _MaxBytesInLine * _LineCnt;
            index += _MaxBytesInLine * _LineCnt;
            for (int i = 0; i < _LineCnt; i++)
                putLine(line, i * _MaxCharsInLine, _MaxCharsInLine);
        }
        if (length > 0)
        {
            int chars = Convert.ToBase64CharArray(bytes, index, length, line, 0);
            int pos = 0;
            while (chars >= _MaxCharsInLine)
            {
                putLine(line, pos, _MaxCharsInLine);
                pos += _MaxCharsInLine;
                chars -= _MaxCharsInLine;
            }
            if (chars > 0)
                putLine(line, pos, chars);
        }
        #region собственная реализация - работает медленнее
        /*
        char[] line = new char[_MaxCharsInLine];
        int lineSz = 0;
        byte[] qGroup = new byte[4];
        while (length > 0)
        {
            while (length >= 3)
            {
                Split3to4(bytes[index++], bytes[index++], bytes[index++], qGroup);
                length -= 3;

                for (int i = 0; i < 4; i++)
                    line[lineSz++] = chars64[qGroup[i]];
                if (lineSz == _MaxCharsInLine)
                {
                    putLine(line, 0, lineSz);
                    lineSz = 0;
                }
            }
        }
        if (length == 2) // неполная группа
        {
            Split3to4(bytes[index], bytes[index + 1], 0, qGroup);
            line[lineSz++] = chars64[qGroup[0]];
            line[lineSz++] = chars64[qGroup[1]];
            line[lineSz++] = chars64[qGroup[2]];
            line[lineSz++] = '=';
        }
        else
        if (length == 1) // неполная группа
        {
            Split3to4(bytes[index], 0, 0, qGroup);
            line[lineSz++] = chars64[qGroup[0]];
            line[lineSz++] = chars64[qGroup[1]];
            line[lineSz++] = '=';
            line[lineSz++] = '=';
        }
        if (lineSz > 0)
            putLine(line, 0, lineSz);*/
        #endregion
    }

    static void Encode(Stream binaryIn, int inBytes, Action<char[], int, int> putLine)
    {
        const int _LineCnt = 200; // оптимальное значение 
        byte[] inBuf = new byte[_MaxBytesInLine * _LineCnt];  // буфер на целое количество строк
        int bufCnt = 0;
        if (inBytes == -1)
        {
            // читаем весь stream до конца
            while (true)
            {
                bufCnt = binaryIn.Read(inBuf, 0, inBuf.Length);
                if (bufCnt == 0)
                    break;
                Encode(inBuf, 0, bufCnt, putLine);
            }
        }
        else
        {
            while (inBytes > 0)
            {
                bufCnt = binaryIn.Read(inBuf, 0, inBytes > inBuf.Length ? inBuf.Length : inBytes);
                if (bufCnt == 0)
                    break;
                inBytes -= bufCnt;
                Encode(inBuf, 0, bufCnt, putLine);
            }
        }
    }

    public static string Encode(byte[] bytes)
    {
        return Convert.ToBase64String(bytes, Base64FormattingOptions.InsertLineBreaks);
    }

    public static void Encode(byte[] bytes, TextWriter writer)
    {
        Encode(bytes, 0, bytes.Length, writer);
    }

    public static void Encode(byte[] bytes, Stream textOut, Encoding encoding)
    {
        Encode(bytes, 0, bytes.Length, textOut, encoding);
    }

    public static string Encode(byte[] bytes, int offset, int length)
    {
        return Convert.ToBase64String(bytes, offset, length, Base64FormattingOptions.InsertLineBreaks);
    }

    public static void Encode(byte[] bytes, int offset, int length, TextWriter writer)
    {
        bool linebrk = false;
        Encode(bytes, offset, length, (cc, idx, len) =>
        {
            if (linebrk)
                writer.WriteLine();
            else
                linebrk = true;
            writer.Write(cc, idx, len);
        });
    }

    public static void Encode(byte[] bytes, int offset, int length, Stream textOut, Encoding encoding)
    {
        using (var writer = new StreamWriter(textOut, encoding, 4096, true))
            Encode(bytes, offset, length, writer);
    }

    public static string Encode(Stream binaryIn)
    {
        return Encode(binaryIn, -1);
    }

    public static void Encode(Stream binaryIn, TextWriter writer)
    {
        Encode(binaryIn, -1, writer);
    }

    public static void Encode(Stream binaryIn, Stream textOut, Encoding encoding)
    {
        Encode(binaryIn, -1, textOut, encoding);
    }

    public static string Encode(Stream binaryIn, int inBytes)
    {
        StringBuilder builder;
        if (inBytes == -1)
        {
            if (binaryIn.CanSeek)
            {
                long size = (binaryIn.Length - binaryIn.Position) * 14 / 10;
                if (size < int.MaxValue)
                    builder = new StringBuilder((int) size);
                else
                    builder = new StringBuilder();
            }
            else
                builder = new StringBuilder();
        }
        else
            builder = new StringBuilder(inBytes * 14 / 10);
        bool linebrk = false;
        Encode(binaryIn, inBytes, (cc, idx, len) =>
        {
            if (linebrk)
                builder.AppendLine();
            else
                linebrk = true;
            builder.Append(cc, idx, len);
        });
        return builder.ToString();
    }

    public static void Encode(Stream binaryIn, int inBytes, TextWriter writer)
    {
        bool linebrk = false;
        Encode(binaryIn, inBytes, (cc, idx, len) =>
        {
            if (linebrk)
                writer.WriteLine();
            else
                linebrk = true;
            writer.Write(cc, idx, len);
        });
    }

    public static void Encode(Stream binaryIn, int inBytes, Stream textOut, Encoding encoding)
    {
        using (var writer = new StreamWriter(textOut, encoding, 4096, true))
            Encode(binaryIn, inBytes, writer);
    }

}
