import re
import string
import os

for thing in string.ascii_letters:
    print thing


def myFunction(*args):
    def isEven(number):
        return number % 2 == 0
    a = map(isEven, *args)
    return a

print myFunction([2, 4, 5])
