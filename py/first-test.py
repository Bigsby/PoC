#! /usr/bin/python

import unittest

class TestClass(unittest.TestCase):

    def test_first(self):
        self.assertEquals(1, 1)

    def test_second(self):
        self.assertEquals(1, 2)

    def test_another(self):
        assert 1 == 2

if __name__ == "__main__":
    unittest.main()