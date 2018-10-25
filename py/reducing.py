#!/usr/bin/env python

def suming(sub, current):
    print "sub: %d current %d" % (sub, current)
    return sub + current

#result = reduce(lambda sub, current: sub + current, [1, 2, 3])
result = reduce(suming, [1, 2, 3])
print result