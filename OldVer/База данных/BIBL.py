

def sort_inc(list):
    for i in range(len(list)-1):
        for j in range(len(list) - i - 1):
            if list[j] > list[j + 1]:
                list[j], list[j + 1] = list[j + 1], list[j]
    return(list)

def sort_dec(len,list):
    for i in range(len - 1):
        for j in range(len - i - 1):
            if list[j] > list[j + 1]:
                list[j], list[j + 1] = list[j + 1], list[j]