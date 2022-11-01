from random import *


class Subscriber:
    """Информация о подписчике"""
    
    name = ''
    surname = ''
    age = 0
    magazine = ''
    payment = 0
    subscription = 0
    
    def __init__(self, name1, surname1, age1, magazine1, payment1, subscription1):
        self.name = name1
        self.surname = surname1
        self.age = age1
        self.magazine = magazine1
        self.payment = payment1
        self.subscription = subscription1


def add_new_sub():
    """Создает нового подписчика"""
    sub = []

    name = input("Введите Имя - ")
    surname = input("Введите Фамилию - ")
    age = input("Введите возраст - ")
    magazine = input("Введите название журанала - ")
    payment = input("Введите сумму оплаты - ")
    subscription = input("Введите срок подписки (от 1 до 12 месяцев) - ")

    sub = sub + [Subscriber(name, surname, age, magazine, payment, subscription)]
    return sub

def add_rand_sub():
    """Создает рандомного подписчика"""
    sub = []
    Names = ["Hermione", "Harry", "Ron", "Draco", "Albus", "Nevil", "Lucius", "Sedric", "Goil", "Tom", "Grum", "Hagrid"]
    Surnames = ["Granger", "Potter", "Uisley", "Malfoi", "Dambldor","Longbotto1m", "Digori", "Nosurname", "Reddle", "Forester"]
    Magazines = ["Gryffindor", "Hufflepuff", "Ravenclaw", "Slytherin"]
    
    name = Names[ randint(0,11) ]
    surname = Surnames[ randint(0,9)]
    magazine = Magazines[ randint(0,3) ]
    
    sub = sub + [ Subscriber(name, surname, randint(12,100 ), magazine, randint(0,100), randint(1,12)) ]
    return sub


def print_sub(d):
    print(d.name, end=(' | '))
    print(d.surname, end=(' | '))
    print(d.age, end=(' | '))
    print(d.magazine, end=(' | '))
    print(d.payment, end=(' | '))
    print(d.subscription)


def print_sub_list( subs ):
    """..."""
    count = 1
    for d in subs:
        print(count, end=(' | '))
        print_sub( d )
        count += 1


def sub2file(sub, filename):
    """..."""
    f = open(filename, "a")

    for d in sub:
        f.write(d.name)
        f.write(", ")
        f.write(d.surname)
        f.write(", ")
        f.write(str(d.age))
        f.write(", ")
        f.write(d.magazine)
        f.write(", ")
        f.write(str(d.payment))
        f.write(", ")
        f.write(str(d.subscription))
        f.write("\n")

    f.close()


def load_database(filename):
    """загружает данные из файла"""
    database = []
    f = open(filename, 'r')
    while True:
        s = f.readline()
        if s == '':
            break
        s = s.split(',')
        d = Subscriber(s[0].strip(), (s[1].strip()),(s[2].strip()),(s[3].strip()),(s[4].strip()),(s[5].strip()))
        database = database + [d]

    f.close()
    return database


def choose(sub):
    while True:
        print('''Желаете ли внести данного попдписчика в базу данных?
        1. Да
        2. Нет
        ''')
        ans1 = int(input())
        if ans1 == 1:
            print('Подписчик добавлен')
            sub2file(sub, 'subscribers.txt')
            break
        elif ans1 == 2:
            break


def subss2file( subs, filename ):
    """..."""
    f = open( filename, "w" )

    for d in subs:
        f.write(d.name)
        f.write(", ")
        f.write(d.surname)
        f.write(", ")
        f.write(str(d.age))
        f.write(", ")
        f.write(d.magazine)
        f.write(", ")
        f.write(str(d.payment))
        f.write(", ")
        f.write(str(d.subscription))
        f.write("\n")

    f.close()


def remove_sub():



DataBase = []
def main():
    while True:
        print("""Введите цифру для
    1. Добавить нового случайного подписчика
    2. Добавить нового подписчка вручную
    3. Загрузить список подписчиков из файла
    4. Показать список подписчиков
    5. Удалить выборочно данные
    6. Редактировать
    7. Сохранить данные в базу
    8. Поиск
    9. Сортировка

    0. -- выход
            """)
        ans = int(input())
        if ans == 1:
            l = add_rand_sub()
            print('Name | Surname | Age | Magazine | Payment | Subscription')
            print_sub_list( l )
            choose(l)
        elif ans == 2:
            l = add_new_sub()
            print('Name | Surname | Age | Magazine | Payment | Subscription')
            print_sub_list(l)
            choose(l)

        elif ans == 3:
            DataBase = load_database('subscribers.txt')
            print("Список подписчиков загружен")
        elif ans == 4:
            print("Список подписчиков:")
            print('Name | Surname | Age | Magazine | Payment | Subscription')
            print_sub_list(DataBase)
        elif ans == 5:
            print('Введите номер подписчика, которого хотите удалить из базы')
            numb = int(input())
            DataBase.remove(DataBase[numb-1])
            print('Name | Surname | Age | Magazine | Payment | Subscription')
            print_sub_list(DataBase)
        elif ans == 7:
            subss2file(DataBase, 'subscribers.txt')
        elif ans == 0:
            break
main()