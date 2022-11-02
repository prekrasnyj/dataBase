from random import *

# class -- составной тип данных
class Debtor:
    """Информация о должнике"""
    # поля класса -- внутр. переменные класса
    name = ''   # имя должника
    summ = 0    # сумма долга

    def __init__(self, name1, summ1):
        self.name = name1
        self.summ = summ1
        
    def to_string(self):
        """возвр. строковое представление данных для отображения на экране"""
        return f"{self.name:40s}, {self.summ:6d}"
    
    # todo: прообразование в компактную строку
    # todo?: прообразование в набор байт
    # todo: проверка данных
    
        

def rand_debtor_list(n):
    """Создаёт список из n должников 
    должник -- значение типа данных Debtor"""
    l = []
    for i in range(n):
        l = l + [ Debtor("Иванов", randint(1000, 10000) ) ]
    return l


def rand_debtor_list2(n):
    """Создаёт список из n должников 
    должник -- значение типа данных Debtor"""
    Names = ["Иванов", "Петров", "Сидоров"]
    l = []
    for i in range(n):
        name = Names[ randint(0,2) ]
        
        if randint(0,1) == 1:
            name = name + 'a'

        l = l + [ Debtor(name, randint(1000, 10000) ) ]
    return l


# def debtors2file( debtors, filename ):
#     """..."""
#     f = open( filename, "w" )
# 
#     for d in debtors:
#         f.write( d.name )
#         f.write(", ")
#         f.write( str(d.summ) )
#         f.write( "\n" )
# 
#     f.close()
    
    
def debtors2file( debtors, filename ):
    """..."""
    f = open( filename, "w", encoding='utf-8' )

    for d in debtors:
        f.write( d.to_string() )
        f.write( "\n" )

    f.close()
    
    
def load_database(filename):
    """загружает данные из файла"""
    database = []
    f = open(filename, 'r', encoding='utf-8')
    while True:
        s = f.readline()
        if s == '':
            break
        s = s.split(',')
        d = Debtor( s[0].strip(),  int(s[1].strip()) )
        database = database + [d]
        
    f.close()
    return database


# todo: сортировка
# todo: поиск
# todo: добавление
# todo: удаление
# todo: редактирование