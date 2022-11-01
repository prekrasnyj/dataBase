class User:
    first_name: str
    last_name: str

    def __init__(self, first_name, last_name):
        self.first_name = first_name
        self.last_name = last_name

    def full_name(self):
        # return self.first_name + self.last_name
        return f"Fullname: {self.first_name} {self.last_name}"


class AgedUser(User):
    age: int

    def __init__(self, first_name, last_name, age):
        super().__init__(first_name, last_name)
        self.age = age

    def full_name(self):
        return super().full_name() + f", Age: {self.age}"


aged_john = AgedUser("John", "Doe", 30)
# aged_john.age = 10
0print(aged_john.full_name(),aged_john.age)