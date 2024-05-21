import keyboard
from threading import Timer
import os

os.system("cls")
# 5-bit sequences to characters
bit_to_char = {
    '00001': 'a', '00010': 'b', '00011': 'c', '00100': 'd',
    '00101': 'e', '00110': 'f', '00111': 'g', '01000': 'h',
    '01001': 'i', '01010': 'j', '01011': 'k', '01100': 'l',
    '01101': 'm', '01110': 'n', '01111': 'o', '10000': 'p',
    '10001': 'q', '10010': 'r', '10011': 's', '10100': 't',
    '10101': 'u', '10110': 'v', '10111': 'w', '11000': 'x',
    '11001': 'y', '11010': 'z',

    '11101': '😊','11110': '❤️', '11111': ' '
}

# key to bit position mappings
key_to_bit = {
    'r'     : '00001',  # a5
    'e'     : '00010',  # a4
    'z'     : '00100',  # a3
    'a'     : '01000',  # a2
    'space' : '10000'   # a1
}

pressed_keys = set()
result = ''

def reset_keys():
    global pressed_keys
    pressed_keys.clear()

def on_key_press(event):
    global pressed_keys, result
    if event.name in key_to_bit:
        pressed_keys.add(event.name)

    elif event.name == 'f':
        result = result[:-1]
        os.system("cls")
        print(result)

def on_key_release(event):
    if event.name in key_to_bit:
        pressed_keys.discard(event.name)

def check_keys():
    global pressed_keys, result

    if pressed_keys:
        buffer = ['0'] * 5
        for key in pressed_keys:
            bit_position = list(key_to_bit.keys()).index(key)
            buffer[4 - bit_position] = '1'

        bit_string = ''.join(buffer)
        char = bit_to_char[bit_string]

        if char:
            result += char
            os.system("cls")
            print(result)

        reset_keys()
    Timer(0.1, check_keys).start()

for key in key_to_bit.keys():
    keyboard.on_press_key(key, on_key_press)
    keyboard.on_release_key(key, on_key_release)

keyboard.on_press_key('f', on_key_press)

# Start the timer to check keys
Timer(0.1, check_keys).start()
keyboard.wait('esc')  # Wait for 'esc' key to exit the program
