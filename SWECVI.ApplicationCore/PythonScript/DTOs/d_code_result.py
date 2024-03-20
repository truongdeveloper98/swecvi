# coding=utf-8

class DCodeResult:
    def __init__(self, d_code = None, result = None, lvavpd = None, lvgls = None, par = None, ref = None, callLevel = None, isLimited = None):
        self.DCode = d_code
        self.Result = result
        self.LVAVPD = lvavpd
        self.LVGLS = lvgls
        self.PAR = par
        self.REF = ref
        self.CallLevel = callLevel
        self.IsLimited = callLevel