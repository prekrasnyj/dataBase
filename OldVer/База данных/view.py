def sort_list_decrease(list):
    for i in range(0, len(list) - 1):
        Max = i
        for j in range(i + 1, len(list)):
            if list[j] > list[Max]:
                Max = j
        list[i], list[Max] = list[Max], list[i]
    return list


def sort_list_increase(list):
    for i in range(0, len(list) - 1):
        Min = i
        for j in range(i + 1, len(list)):
            if list[j] < list[Min]:
                Min = j
        list[i], list[Min] = list[Min], list[i]
    return list


l = [5,3,2,3,1,2]
a = ['a','b','t','c','s','r']
print(sort_list_decrease(a))
print(sort_list_increase(a))