# coding=utf-8

def CalculateBSA(weight, height):
    if weight == None or height == None:
        return None

    if weight == 0 or height == 0:
        return None

    height = height * 100 # m => cm

    return pow(weight, 0.425) * pow(height, 0.725) * 0.007184