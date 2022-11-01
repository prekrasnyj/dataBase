import random
import nltk

BOT_CONFIG = {
    'intents': {

        'hello': {
            'examples': ['Привет', 'Добрый день', 'Шалом', 'Привет, бот'],
            'responses': ['Привет, человек!', 'Здравствуй, человечишка!', 'Доброго времени суток! с:']
        },
        'bye': {
            'examples': ['Пока', 'Досвидания', 'До свидания', 'Всего доброго'],
            'responses': ['Еще увидимся', 'Если что, я всегда тут']
        },
        'name': {
            'examples': ['Как тебя зовут', 'Скажи свое имя', 'Представься'],
            'responses': ['Меня зовут Саша']
        },
    },
        'falure_phrase': [
            'Непонятно. Перефразируйте, пожалуйста!',
            'Я еще только учусь. Спрсоите что-нибудь другое',
            'Повторите попытку, пожалуйста'
        ]
}



# Очищает фразу пользователя от лишних символов и переводит в нижний регистр
def clear_phrase(phrase):
    phrase = phrase.lower()

    alphabet = 'абвгдеёжзийклмнопрстуфхцчшщъыбэюя- '
    result = ''.join([symbol for symbol in phrase if symbol in alphabet])

#    result = ''
#    for symbol in phrase:
#        if symbol in alphabet:
#            result += symbol

    return phrase


# Определяет по реплике к какому намерению относится
def classify_intent(replica):
    for intent, intent_data in BOT_CONFIG['intents'].items():
        for example in intent_data['examples']:
            if example == replica:
                return  intent
print(classify_intent('Шалом'))

def get_answer_by_intent(intent):
    if intent in BOT_CONFIG['intents']:
        responses = BOT_CONFIG['intents'][intent]['responses']
    return random.choice(responses)


def generate_answer(replica):
    #TODO НА 3 ДЕНЬ
    return


def get_falure_phrase():
    falure_phrase = BOT_CONFIG['falure_phrase'    ]
    return random.choice(falure_phrase)


def bot(replica):
    # NLU
    intent = classify_intent(replica)

    # Answer generation
    # Выбор заготовленной реплики
    if intent is not None:
        answer = get_answer_by_intent(intent)
        if answer is not None:
            return answer

    # Вызов генеративной модели
    answer = generate_answer(replica)
    if answer is not None:
        return answer

    # Берем заглушку
    return get_falure_phrase()

    answer = replica
    return answer

print(bot('добрый вечер'))