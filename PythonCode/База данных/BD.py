EXIT = True
while EXIT == True:

    print('1) Просмотр')
    print('2) Редактирование')
    print('3) Выход')
    choice = int(input())
    #Выход из программы
    if   choice == 3:
        EXIT = False

    #Входим в меню просмотра
    elif choice == 1:
        EXIT2 = True
        while EXIT2 == True:
            print('1) В разработке')
            print('2) Назад')
            choice2 = int(input())
            if choice2 == 2:
                EXIT2 = False

    # Входим в меню редактирования
    elif choice == 2:
        EXIT2 = True
        while EXIT2 == True:
            print('1) В разработке')
            print('2) Назад')
            choice2 = int(input())
            if choice2 == 2:
                EXIT2 = False

    #Заставляем выбрать нужный из вариантов
    else:
        print('Попробуй еще раз')
        continue