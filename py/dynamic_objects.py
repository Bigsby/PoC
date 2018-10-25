#!/usr/bin/python3
"""This is a test deserialization
"""

import json
import inspect

class DataObject(object):
    def __init__(self, jsonData):
        self.__dict__ = json.loads(jsonData)
    def __setattr__(self, attr, value):
        print("Setting %s" % attr)
        super(DataObject, self).__setattr__(attr, value)

class TheData(DataObject):
    pass


JSON_CONTENT = '{"name":"Jonh Cleese","value":2}'
o = TheData(JSON_CONTENT)
setattr(o, "anotherprop", "anotherValue")
print(o.name)
print(getattr(o, "value"))
print(o.anotherprop)
print(o.__dict__)
print(inspect.getmembers(o, lambda m: not inspect.isbuiltin(m)))
