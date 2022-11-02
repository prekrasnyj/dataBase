import pickle

#Создаем профиль подписчика
prof = []
prof.append(input('Введите имя'))
prof.append(input('Введите Возраст'))
prof.append(input('Введите Город'))

f = open(r"Fil.txt","wb")
pickle.dump(prof, f)
f.close()