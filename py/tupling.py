#!/usr/bin/env python

import uuid

items = tuple(
            str(uuid.uuid4()) for _ in range(8)
        )

def fun(item1, item2):
    print "1 %s" % item1
    print "2 %s" % item2

print items
fun(*items[:2])

one, two, three = ("one", "two", "three")
print one
print two
print three

four, five, six = [ "four", "five", "six" ]
print four
print five
print six