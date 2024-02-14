import unittest
from gilded_rose import Item, GildedRose

class GildedRoseTest(unittest.TestCase):
        def test_aged_brie(self):
            item = Item ("Aged Brie", 10, 20)
            gilded_rose = GildedRose([item])

            gilded_rose.update_quality()
            self.assertEqual(item.sell_in, 9)
            self.assertEquals(item.quality, 21)

        def test_sulfuras(self):
            item = Item("Sulfuras", 10, 80)
            gilded_rose = GildedRose([item])

            gilded_rose.update_quality()
            self.assertNotEqual(item.sell_in, 9)
            self.assertEqual(item.sell_in, 10)
            self.assertEquals(item.quality, 80)


if __name__ == '__main__':
    unittest.main()