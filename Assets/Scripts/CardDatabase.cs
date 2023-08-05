using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardDatabase : MonoBehaviour
{
    static Card wall = new Card(10,10,10,10,100,0,"Wall","back");
    static Card empty = new Card(0,0,0,0, 100, 0, "Empty", "back");

    public List<Card> getCardList()
    {
        List<Card> cardList = new List<Card>();

        cardList.Add(new Card(5,3,1,3, 1, 0, "Bite Bug", "l1BiteBug"));
        cardList.Add(new Card(5,3,2,1, 1, 0, "Blobra", "l1Blobra"));
        cardList.Add(new Card(1,1,2,6, 1, 0, "Bloud Soul", "l1BloudSoul"));
        cardList.Add(new Card(3,2,4,4, 1, 0, "Caterchipillar", "l1Caterchipillar"));
        cardList.Add(new Card(6,1,2,2, 1, 6, "Cockatrice", "l1Cockatrice"));
        cardList.Add(new Card(1,5,3,2, 1, 1, "Fastitocalon F", "l1Fastitocalonf"));
        cardList.Add(new Card(3,1,5,1, 1, 0, "Funguar", "l1Funguar"));
        cardList.Add(new Card(5,4,1,1, 1, 0, "Geezard", "l1Geezard"));
        cardList.Add(new Card(1,5,1,4, 1, 0, "Gesper", "l1Gesper"));
        cardList.Add(new Card(2,1,6,1, 1, 0, "Red Bat", "l1RedBat"));

        cardList.Add(new Card(5,1,5,3, 2, 4, "Anacondaur", "l2Anacondaur"));
        cardList.Add(new Card(3,4,3,5, 2, 0, "Belhelmel", "l2Belhelmel"));
        cardList.Add(new Card(3,2,6,2, 2, 0, "Buel", "l2Buel"));
        cardList.Add(new Card(2,2,5,5, 2, 6, "Creeps", "l2Creeps"));
        cardList.Add(new Card(3,1,6,4, 2, 8, "Glacial Eye", "l2GlacialEye"));
        cardList.Add(new Card(3,2,5,5, 2, 0, "Grand Mantis", "l2GrandMantis"));
        cardList.Add(new Card(1,1,7,3, 2, 0, "Grat", "l2Grat"));
        cardList.Add(new Card(2,4,4,5, 2, 6, "Grendel", "l2Grendel"));
        cardList.Add(new Card(7,2,3,1, 2, 0, "Jelleye", "l2Jelleye"));
        cardList.Add(new Card(4,3,5,3, 2, 0, "Mesmerize", "l2Mesmerize"));
        cardList.Add(new Card(5,3,5,2, 2, 7, "Thrustaevis", "l2Thrustaevis"));

        cardList.Add(new Card(5,2,7,3, 3, 1, "Abyss Worm", "l3AbyssWorm"));
        cardList.Add(new Card(6,3,6,1, 3, 1, "Armadodo", "l3Armadodo"));
        cardList.Add(new Card(3,2,6,6, 3, 0, "Cactuar", "l3Cactuar"));
        cardList.Add(new Card(2,4,4,7, 3, 2, "Death Claw", "l3DeathClaw"));
        cardList.Add(new Card(3,5,7,1, 3, 1, "Fastitocalon", "l3Fastitocalon"));
        cardList.Add(new Card(2,6,6,3, 0, 0, "Forbidden", "l3Forbidden"));
        cardList.Add(new Card(3,6,5,3, 3, 0, "Ochu", "l3Ochu"));
        cardList.Add(new Card(4,6,5,2, 3, 2, "SAM08G", "l3SAM08G"));
        cardList.Add(new Card(3,1,7,5, 3, 8, "Snow Lion", "l3SnowLion"));
        cardList.Add(new Card(4,6,3,4, 3, 0, "Tonberry", "l3Tonberry"));
        cardList.Add(new Card(5,5,3,5, 3, 4, "Tri Face", "l3TriFace"));

        cardList.Add(new Card(6,5,4,5, 4, 1, "Adamantoise", "l4Adamantoise"));
        cardList.Add(new Card(7,6,1,4, 4, 6, "Blitz", "l4Blitz"));
        cardList.Add(new Card(3,2,6,7, 4, 4, "Blue Dragon", "l4BlueDragon"));
        cardList.Add(new Card(3,7,2,6, 4, 2, "Bomb", "l4Bomb"));
        cardList.Add(new Card(7,2,6,6, 4, 0, "Elastoid", "l4Elastoid"));
        cardList.Add(new Card(3,5,7,4, 4, 2, "Hexadragon", "l4Hexadragon"));
        cardList.Add(new Card(6,7,3,3, 4, 0, "Imp", "l4Imp"));
        cardList.Add(new Card(4,4,7,4, 4, 0, "Torama", "l4Torama"));
        cardList.Add(new Card(7,6,4,2, 4, 0, "T Rexaur", "l4TRexaur"));
        cardList.Add(new Card(7,3,2,6, 4, 0, "Turtapod", "l4Turtapod"));
        cardList.Add(new Card(5,5,6,4, 4, 0, "Vysage", "l4Vysage"));
        cardList.Add(new Card(6,3,7,1, 4, 0, "Wendigo", "l4Wendigo"));

        cardList.Add(new Card(7,6,3,5, 5, 0, "Behemoth", "l5Behemoth"));
        cardList.Add(new Card(7,6,6,2, 5, 0, "Biggs Wedge", "l5BiggsWedge"));
        cardList.Add(new Card(3,6,7,5, 5, 3, "Chimera", "l5Chimera"));
        cardList.Add(new Card(6,3,5,7, 5, 0, "Elnoyle", "l5Elnoyle"));
        cardList.Add(new Card(4,5,5,7, 5, 0, "GIM47N", "l5GIM47N"));
        cardList.Add(new Card(5,5,6,6, 5, 0, "Iron Giant", "l5IronGiant"));
        cardList.Add(new Card(2,7,7,4, 5, 4, "Malboro", "l5Malboro"));
        cardList.Add(new Card(1,10,3,2, 5, 0, "Pu Pu", "l5PuPu"));
        cardList.Add(new Card(4,2,7,7, 5, 2, "Ruby Dragon", "l5RubyDragon"));
        cardList.Add(new Card(4,6,4,7, 5, 0, "Tonberry King", "l5TonberryKing"));

        cardList.Add(new Card(5,8,6,4, 6, 0, "Abaddon", "l6Abaddon"));
        cardList.Add(new Card(4,8,7,3, 6, 7, "Elvoret", "l6Elvoret"));
        cardList.Add(new Card(4,8,2,8, 6, 0, "Fujin Raijin", "l6FujinRaijin"));
        cardList.Add(new Card(3,8,1,8, 6, 4, "Gerogero", "l6Gerogero"));
        cardList.Add(new Card(5,2,7,8, 6, 0, "Granaldo", "l6Granaldo"));
        cardList.Add(new Card(2,2,8,8, 6, 0, "Iguion", "l6Iguion"));
        cardList.Add(new Card(1,5,7,8, 6, 0, "Krysta", "l6Krysta"));
        cardList.Add(new Card(4,5,6,8, 6, 0, "NORG", "l6NORG"));
        cardList.Add(new Card(8,8,1,4, 6, 0, "Oilboyle", "l6Oilboyle"));
        cardList.Add(new Card(6,8,4,5, 6, 0, "Trauma", "l6Trauma"));
        cardList.Add(new Card(3,8,4,7, 6, 0, "X-ATM092", "l6X-ATM092"));

        cardList.Add(new Card(5,7,5,8, 7, 0, "BGH251F2", "l7BGH251F2"));
        cardList.Add(new Card(7,8,1,7, 7, 0, "Catoblepas", "l7Catoblepas"));
        cardList.Add(new Card(8,6,5,6, 7, 0, "Gargantua", "l7Gargantua"));
        cardList.Add(new Card(4,8,8,4, 7, 0, "Jumbo Cactuar", "l7JumboCactuar"));
        cardList.Add(new Card(3,6,8,7, 7, 0, "Mobile Type 8", "l7MobileType8"));
        cardList.Add(new Card(8,4,8,4, 7, 0, "Propagator", "l7Propagator"));
        cardList.Add(new Card(7,8,6,4, 7, 0, "Red Giant", "l7RedGiant"));
        cardList.Add(new Card(8,3,8,5, 7, 0, "Sphinxaur", "l7Sphinxaur"));
        cardList.Add(new Card(4,8,8,5, 7, 0, "Tiamat", "l7Tiamat"));
        cardList.Add(new Card(8,7,7,2, 7, 0, "Ultima Weapon", "l7UltimaWeapon"));

        cardList.Add(new Card(3,6,9,7, 8, 0, "Angelo", "l8Angelo"));
        cardList.Add(new Card(4,4,9,8, 8, 0, "Chocobo", "l8Chocobo"));
        cardList.Add(new Card(9,4,4,8, 8, 0, "Chubby Chocobo", "l8ChubbyChocobo"));
        cardList.Add(new Card(6,7,3,9, 8, 0, "Gilgamesh", "l8Gilgamesh"));
        cardList.Add(new Card(8,6,9,2, 8, 2, "Ifrit", "l8Ifrit"));
        cardList.Add(new Card(2,3,9,9, 8, 0, "Mini Mog", "l8MiniMog"));
        cardList.Add(new Card(9,5,9,2, 8, 1, "Minotaur", "l8Minotaur"));
        cardList.Add(new Card(4,9,2,9, 8, 6, "Quezacotl", "l8Quezacotl"));
        cardList.Add(new Card(9,1,5,9, 8, 1, "Sacred", "l8Sacred"));
        cardList.Add(new Card(9,7,6,4, 8, 8, "Shiva", "l8Shiva"));
        cardList.Add(new Card(2,9,8,6, 8, 0, "Siren", "l8Siren"));

        cardList.Add(new Card(2,10,9,4, 9, 5, "Alexander", "l9Alexander"));
        cardList.Add(new Card(6,8,10,2, 9, 0, "Bahamut", "l9Bahamut"));
        cardList.Add(new Card(4,4,8,10, 9, 0, "Carbuncle", "l9Carbuncle"));
        cardList.Add(new Card(10,4,7,6, 9, 0, "Cerberus", "l9Cerberus"));
        cardList.Add(new Card(3,10,5,8, 9, 0, "Diablos", "l9Diablos"));
        cardList.Add(new Card(10,1,3,10, 9, 4, "Doomtrain", "l9DoomTrain"));
        cardList.Add(new Card(10,4,4,9, 9, 0, "Eden", "l9Eden"));
        cardList.Add(new Card(7,10,7,1, 9, 3, "Leviathan", "l9Leviathan"));
        cardList.Add(new Card(5,10,8,3, 9, 0, "Odin", "l9Odin"));
        cardList.Add(new Card(7,1,10,7, 9, 7, "Pandemona", "l9Pandemona"));
        cardList.Add(new Card(10,2,7,7, 9, 2, "Phoenix", "l9Phoenix"));

        cardList.Add(new Card(3,10,10,3, 10, 0, "Edea", "l10Edea"));
        cardList.Add(new Card(10,6,2,9, 10, 0, "Irvine", "l10Irvine"));
        cardList.Add(new Card(10,7,6,6, 10, 0, "Kiros", "l10Kiros"));
        cardList.Add(new Card(9,10,5,3, 10, 0, "Laguna", "l10Laguna"));
        cardList.Add(new Card(2,6,9,10, 10, 0, "Quistis", "l10Quistis"));
        cardList.Add(new Card(10,10,4,2, 10, 0, "Rinoa", "l10Rinoa"));
        cardList.Add(new Card(4,9,6,10, 10, 0, "Seifer", "l10Seifer"));
        cardList.Add(new Card(4,8,10,6, 10, 0, "Selphie", "l10Selphie"));
        cardList.Add(new Card(9,4,10,6, 10, 0, "Squall", "l10Squall"));
        cardList.Add(new Card(8,7,10,2, 10, 0, "Ward", "l10Ward"));
        cardList.Add(new Card(6,5,8,10, 10, 0, "Zell", "l10Zell"));

        return cardList;
    }

    public Card getWall()
    {
        return wall;
    }

    public Card getEmpty()
    {
        return empty;
    }
}
