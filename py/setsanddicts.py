#!/usr/bin/env python

s = set()
print type(s)
print isinstance(s, set)

d = {}
print type(d)
print isinstance(d, dict)

print d.has_key(1)
d[1] = 0
print d[1]
d[1] += 1
print d[1]

d[2] = set()
d[2].add("me")
print "len(d[2]) %d" % len(d[2])
print "me" in d[2]
d[2].add("you")
print len(d[2])
