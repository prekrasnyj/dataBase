# 1. Записать в файл список из имён должников и сумм долга
# 2. Считать эти данные из файла
# 3. Вычислить общую сумму долга


from database import *
from contr_view import *

# здесь функции для Controler и View

# для хранения БД
DataBase = []


def main():
    global DataBase
    while True:
        print("""Введите цифру для
    1. -- загр. данные из файла
    11. - сгенерировать данные
    2. -- показать все данные
    3. -- добавить
    4. -- удалить
    5. -- редактировать
    6. -- сохранить
    7. -- поиск
    8. -- сортировка

    0. -- выход
        """)
        
        ans = input("> ")
        if ans == '1':
            DataBase = load_database('batabase.txt')
        elif ans == '11':
            n = int( input("Введите число строк таблицы, которые нужно создать") )
            DataBase = rand_debtor_list2(n)
        elif ans == '2':
            print_debtors_list( DataBase )
        elif ans == '3':
            add_debtor()
        elif ans == '6':
            debtors2file( DataBase, 'batabase.txt' )
        elif ans == '0':
            break
        # todo    
        
        
    


main()


# todo: дописать main
# todo: не использовать закодированое имя файла