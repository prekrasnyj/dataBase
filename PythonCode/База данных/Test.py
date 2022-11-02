import random

#Функция автоматически создает список из 'x' элемементов
def auto(x):
    a = [random.randint(0, 10) for i in range(x)]
    return a


#Функция вручную создает список из 'x' элемементов
def manually(x):
    cnt = 0
    a = []
    for i in range(0, x):  # Создание массива 'а'
        cnt += 1
        print('Введите ', cnt, ' элемент матрицы = ', end='')
        a.append(float(input()))
    return a


#Функция сортирует готовый список по возрастанию методом пузырька
def bubble_sort_inc(list):
    for i in range(len(list)-1):
        for j in range(len(list) - i - 1):
            if list[j] > list[j + 1]:
                list[j], list[j + 1] = list[j + 1], list[j]
    return(list)


#Функция сортирует готовый список по убыванию методом пузырька
def bubble_sort_dec(list):
    for i in range(len(list)-1):
        for j in range(len(list) - i - 1):
            if list[j] < list[j + 1]:
                list[j], list[j + 1] = list[j + 1], list[j]
    return(list)

n = int(input())
a = (auto(n))
print(a)
print(bubble_sort_inc(a))
print(bubble_sort_dec(a))

