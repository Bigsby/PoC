#!/usr/bin/env python

stem_ids = [
    "883e96ae-afa7-4f6b-a017-93272b215865",
    "e01e4554-05c5-412e-b660-102c8af5c0de",
    "892ae834-0ab9-4bb6-b4eb-6dc5059351b6",
    "4422a7aa-8597-4c09-a986-09a93c35d9a4",
    "244321ea-ed61-4782-bb6a-fc3647ddf0d3",
    "1a1c411f-014d-4cc2-9dd1-03a4b1ceebbd",
    "1bbf3113-6b97-440a-9c8b-72bf5abdd7c7",
    "de422e78-2110-429c-a003-d810cb596b8c"
]
print stem_ids

no_ = map(lambda s: s.replace("-", ""), stem_ids)
print no_

ordered = sorted(no_)
print ordered

firstThree = ordered[:3]


concatenated = reduce(lambda so_far, current: so_far + current, ordered)
#print concatenated

import hashlib
md5 = hashlib.md5(concatenated)
result = md5.hexdigest()
print result

print "a",
print "a",
print "a",
print "a",
print "a",