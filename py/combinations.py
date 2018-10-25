#!/usr/bin/python

l = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12]

combinations = 0
for i in range(0, len(l)):
    for j in range(i+1, len(l)):
        for k in range(j+1, len(l)):
            print "%d %d %d" % (l[i], l[j], l[k])
            combinations += 1

print "%d combinations" %combinations