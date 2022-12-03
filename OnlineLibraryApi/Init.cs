﻿using BusinessLayer.BusinessObjects;
using BusinessLayer.Managers.Interfaces;

namespace OnlineLibraryApi
{
    internal interface IInit
    {
        void InitEntres();
    }

    internal class Init : IInit
    {
        private IBookManager _bookManager;
        private IUserManager _userManager;
        private IAdminManager _adminManager;

        public Init(IBookManager bookManager, IUserManager userManager, IAdminManager adminManager)
        {
            _bookManager = bookManager;
            _userManager = userManager;
            _adminManager = adminManager;
        }

        public void InitEntres()
        {
            _bookManager.DeleteAllBooks();
            _userManager.DeleteAllUsers();

            CreateAdminUser();
            CreateUserAndBooks();
        }

        private void CreateAdminUser()
        {
            _userManager.CreateUser(true, new User
            {
                AccountState = AccountState.Active,
                Firstname = "admin",
                Surname = "admin",
                Username = "admin",
                Password = "admin123",
                Address = new Address
                {
                    Zip = "67801",
                    City = "Blansko",
                    Street = "Dvorska",
                    DescriptiveNumber = "120"
                },
                Pin = "0101010008",
                IsAdmin = true
            });
        }

        private void CreateUserAndBooks()
        {
            var books = new string[]
            {
                "Abhorsen Trilogy|Garth Nix",
                "Acacia: The War With The Mein|David Anthony Durham",
                "Adam Binder|David R. Slayton",
                "Aegypt|John Crowley",
                "Akata Witch|Nnedi Okorafor",
                "Akata Warrior|Nnedi Okorafor",
                "Aladore|Henry Newbolt",
                "Alice in Wonderland|Lewis Carroll",
                "Alphabet of Thorn|Patricia McKillip",
                "The Tales of Alvin Maker|Orson Scott Card",
                "An Ember in the Ashes|Sabaa Tahir",
                "The Amazing Adventures of Kavalier & Clay|Michael Chabon",
                "The Amber|Roger Zelazny",
                "American Gods|Neil Gaiman",
                "Among Others|Jo Walton",
                "An Awfully Beastly Business|David Sinden, Matthew Morgan, and Guy Macdonald",
                "The Anubis Gates|Tim Powers",
                "Apprentice Adept|Piers Anthony",
                "Arafel duo|C. J. Cherryh",
                "The Artefacts of Power|Maggie Furey",
                "Artemis Fowl|Eoin Colfer.",
                "At Swim-Two-Birds|Flann O'Brien",
                "At the Back of the North Wind|George MacDonald",
                "The Bards of Bone Plain|Patricia A. McKillip",
                "Barsoom|Edgar Rice Burroughs",
                "The Bartimaeus Trilogy|Jonathan Stroud",
                "Bas-Lag|China Miéville",
                "The Battle of Apocalypse|Eduardo Spohr",
                "Beauty|Robin McKinley",
                "Beauty|Sheri S. Tepper",
                "Beautiful Darkness (novel)|Kami Garcia and Margaret Stohl",
                "Beautiful Creatures (book)|Kami Garcia and Margaret Stohl",
                "The Belgariad|David Eddings",
                "The Bell at Sealey Head|Patricia A. McKillip",
                "Beyond the Golden Stair|Hannes Bok",
                "Beyonders: A World Without Heroes|Brandon Mull",
                "Beyond the Spiderwick Chronicles|Holly Black",
                "The BFG|Roald Dahl",
                "The Bitterbynde trilogy|Cecilia Dart-Thornton",
                "Black Blossom|Boban Knežević",
                "The Black Company|Glen Cook, a nested set of sub-series",
                "The Black Jewels trilogy|Anne Bishop",
                "The Black Magician (novel series)|Trudi Canavan",
                "The Black Swan|Mercedes Lackey",
                "The Black Tides of Heaven|JY Yang",
                "Bloodring|Faith Hunter",
                "Blood Song|Anthony Ryan[3]",
                "The Blue Star|Fletcher Pratt",
                "The Blue Sword|Robin McKinley",
                "Boggart|Susan Cooper",
                "Bone Dance|Emma Bull",
                "The Book of Atrix Wolfe|Patricia McKillip",
                "Book of Enchantments|Patricia C. Wrede",
                "The Book of the New Sun|Gene Wolfe",
                "The Book of Stars|Erik L'Homme",
                "The Books of Abarat|Clive Barker",
                "Brak the Barbarian|John Jakes",
                "Brown Girl in the Ring|Nalo Hopkinson",
                "Bloodlines|Richelle Mead",
                "Callipygia|Lin Carter",
                "Captain Slaughterboard Drops Anchor|Mervyn Peake",
                "Cats Have No Lord|Will Shetterly",
                "The Changeling Sea|Patricia A. McKillip",
                "Chapel of Ease|Alex Bledsoe",
                "Charlie and the Chocolate Factory|Roald Dahl",
                "Charlie Bone|Jenny Nimmo",
                "The Chathrand Voyage Quartet|Robert V.S. Redick",
                "Chernevog|C. J. Cherryh",
                "Children of the Lamp|P.B. Kerr",
                "Children of Blood and Bone|Tomi Adeyemi",
                "Chimera|John Barth",
                "Chrestomanci|Diana Wynne Jones",
                "Chronicles of an Age of Darkness|Hugh Cook",
                "Chronicles of Ancient Darkness|Michelle Paver",
                "Chronicles of Brothers|Wendy Alec",
                "The Chronicles of Narnia|C.S. Lewis - see at N, below",
                "The Chronicles of Prydain|Lloyd Alexander",
                "Chronicles of The Raven|James Barclay",
                "The Chronicles of Thomas Covenant|Stephen R. Donaldson - see at T,",
                "The Circus of Dr. Lao|Charles G. Finney",
                "City of Bones|Martha Wells",
                "The City of Brass (novel)|S. A. Chakraborty",
                "The Claidi Journals|Tanith Lee",
                "Coldfire Trilogy|Celia S. Friedman",
                "Cold Tom|Sally Prue",
                "Conan|Robert E. Howard and others",
                "Coraline|Neil Gaiman",
                "The Cottingley Cuckoo|Alison Littlewood",
                "The Cottingley Secret|Hazel Gaynor",
                "The Court of the Air|Stephen Hunt",
                "The Craft Sequence|Max Gladstone",
                "The Crock of Gold|James Stephens",
                "The Cygnet and the Firebird|Patricia A. McKillip",
                "The Dagger and the Coin|Daniel Abraham",
                "Dalemark Quartet|Diana Wynne Jones",
                "The Dalkey Archive|Flann O'Brien",
                "Damiano|R. A. MacAvoy",
                "The Dark Artifices trilogy|Cassandra Clare",
                "The Dark Is Rising sequence|Susan Cooper",
                "The Dark Tower|Stephen King",
                "Daughter of the Lioness|Tamora Pierce",
                "The Death of the Necromancer|Martha Wells",
                "Deltora|Emily Rodda",
                "The Deptford Mice|Robin Jarvis",
                "Deryni novels|Katherine Kurtz",
                "Descent into Hell|Charles Williams",
                "Dirk Gently|Douglas Adams",
                "Discworld|Terry Pratchett",
                "The Divine Cities|Robert Jackson Bennett",
                "The Door in the Hedge|Robin McKinley",
                "The Door Within Trilogy|Wayne Thomas Batson",
                "Dorothea Dreams|Suzy McKee Charnas",
                "The Dragon House|Darrell Schweitzer",
                "The Dragonslayer's Apprentice|David Calder",
                "Dragoncharm trilogy|Graham Edwards",
                "The Dragon's Familiar|Lawrence Jeffrey Cohen",
                "Dragonlance Chronicles trilogy|Margaret Weis and Tracy Hickman",
                "Dragon Raja|Lee Yeongdo",
                "Dragon Rider|Cornelia Funke",
                "The Dragonbone Chair|Tad Williams",
                "The Dragonology Chronicles: The Dragon's Eye|Dugald Steer",
                "Dragonriders of Pern|Anne McCaffrey",
                "Dragonrouge|Lin Carter",
                "Dragonsbane|Barbara Hambly",
                "Dragonvarld trilogy|Margaret Weis",
                "The Dreamwalker's Child|Steve Voake",
                "The Drawing of the Dark|Tim Powers",
                "The Dreamstone|C. J. Cherryh",
                "The Dresden Files|Jim Butcher",
                "Drizzt Do'Urden novels|R. A. Salvatore",
                "The Dying Earth|Jack Vance",
                "Ealdwood|C. J. Cherryh",
                "Earthsea|Ursula K. Le Guin",
                "The Edge Chronicles|Paul Stewart and Chris Riddell",
                "Elantris|Brandon Sanderson",
                "The Elenium|David Eddings",
                "Elric of Melnibone|Michael Moorcock",
                "Ella Enchanted|Gail Carson Levine",
                "The Empire Trilogy|Raymond E. Feist and Janny Wurts",
                "The Enchanted Castle|E. Nesbit",
                "Enchanted Forest|Patricia C. Wrede",
                "Eon: Dragoneye Reborn|Alison Goodman",
                "Eragon|Christopher Paolini",
                "Eldest|Christopher Paolini",
                "Excalibur|Sanders Anne Laubenthal",
                "Expecting Someone Taller|Tom Holt",
                "The Eyes of the Dragon|Stephen King",
                "Fablehaven|Brandon Mull",
                "Faery in Shadow|C. J. Cherryh",
                "The Face in the Frost|John Bellairs",
                "The Faerie Wars Chronicles|Herbie Brennan",
                "Fafhrd and the Gray Mouser|Fritz Leiber",
                "The Fairies of Sadieville|Alex Bledsoe",
                "Fantastic Beasts and Where to Find Them|J.K. Rowling",
                "Farsala trilogy|Hilari Bell",
                "The Farseer Trilogy|Robin Hobb",
                "The Fates of the Princes of Dyfed|Kenneth Morris",
                "Fearsome Creatures of the Lumberwoods|William T. Cox",
                "A Fine and Private Place|Peter S. Beagle",
                "The Fionavar Tapestry|Guy Gavriel Kay",
                "Fisher King|Tim Powers",
                "Fledgling|Octavia Butler",
                "Flesh and Fire|Laura Anne Gilman",
                "Flying Dutch|Tom Holt",
                "The Fool on the Hill|Matt Ruff",
                "The Forgotten Beasts of Eld|Patricia McKillip",
                "Fortress|C. J. Cherryh",
                "Fourth Mansions|R. A. Lafferty",
                "Garrett|Glen Cook",
                "Gather Her Round|Alex Bledsoe",
                "Gezeitenwelt|Magus Magellan",
                "The Ghost and The Goth|Stacey Kade",
                "Ghost Blows Out the Light|Zhang Muye",
                "Ghostwritten|David Mitchell",
                "Giant of World's End|Lin Carter",
                "The Girl in a Swing|Richard Adams",
                "Gloriana, or The Unfulfill'd Queen|Michael Moorcock",
                "The Goblin Emperor|Katherine Addison",
                "The Goblin Mirror|C. J. Cherryh",
                "Gods of Jade and Shadow|Silvia Moreno-Garcia",
                "Gods upon a time|Dai Idris and Martin Newman",
                "The Golem and the Jinni|Helene Wecker",
                "Gormenghast|Mervyn Peake - see at T (Titus), below",
                "Graceling|Kristin Cashore",
                "The Great God Pan|Arthur Machen",
                "The Green Child|Herbert Read",
                "The Green Round|Arthur Machen",
                "Green Rider|Kristen Britain",
                "Grendel|John Gardner",
                "Grishaverse|Leigh Bardugo",
                "Guardians of Ga'Hoole|Kathryn Lasky",
                "The Hagwood Books|Robin Jarvis",
                "The Halfblood Chronicles|Mercedes Lackey and Andre Norton",
                "Haroun and the Sea of Stories|Salman Rushdie",
                "Harry Potter|J. K. Rowling",
                "Hart's Hope|Orson Scott Card",
                "The Haunted Woman|David Lindsay",
                "The Haunting of Hill House|Shirley Jackson",
                "The Heart of What Was Lost|Tad Williams",
                "The Heavenly Fox|Richard Parks",
                "Here Comes the Sun|Tom Holt",
                "Hereafter, and After|Richard Parks",
                "Heroes of the Valley|Jonathan Stroud",
                "The Heroes of Olympus|Rick Riordan",
                "The Hidden People|Alison Littlewood",
                "The Hill of Dreams|Arthur Machen",
                "His Dark Materials|Philip Pullman",
                "History of The Vollplaen|Daniel Johnson",
                "The Hobbit|J. R. R. Tolkien",
                "The Hounds of the Morrigan|Pat O'Shea",
                "The House on the Borderland|William Hope Hodgson",
                "House of Night|P.C. Cast",
                "The House on Parchment Street|Patricia A. McKillip",
                "The House with a Clock in Its Walls|John Bellairs",
                "Howl's Moving Castle|Diana Wynne Jones",
                "The Hum and the Shiver|Alex Bledsoe",
                "The Hundred Thousand Kingdoms|N. K. Jemisin",
                "Hush, Hush|Becca Fitzpatrick",
                "The Idylls of the Queen|Phyllis Ann Karr",
                "Ile-Rien|Martha Wells",
                "The Immortals|Tamora Pierce",
                "Incarnations of Immortality|Piers Anthony",
                "The Incorruptibles|John Hornor Jacobs",
                "Inda (novel)|Sherwood Smith",
                "In the Forests of Serre|Patricia McKillip",
                "The Infernal Desire Machines of Doctor Hoffman (aka The War of Dreams)|Angela Carter",
                "Ingo|Helen Dunmore",
                "The Inheritance Cycle|Christopher Paolini",
                "Inheritance|Steven Savile",
                "Ink Exchange | Wicked Lovely|Melissa Marr",
                "Inkheart Trilogy|Cornelia Funke",
                "The Invisible Library|Genevieve Cogman",
                "Islandia|Austin Tappan Wright",
                "The Infernal Devices trilogy|Cassandra Clare",
                "Jade City|Fonda Lee",
                "James and the Giant Peach|Roald Dahl",
                "Jonathan Strange & Mr. Norrell|Susanna Clarke",
                "Journalists (novel)|Sergei Aman",
                "Kai Lung|Ernest Bramah",
                "Kandide and the Secret of the Mists|Diana S. Zimmerman",
                "The Kane Chronicles|Rick Riordan",
                "Kellory the Warlock|Lin Carter",
                "Keeper of the Lost Cities|Shannon Messenger",
                "Kesrick|Lin Carter",
                "The Keys to the Kingdom|Garth Nix",
                "The Khaavren Romances|Steven Brust",
                "Khaled: A Tale of Arabia|F. Marion Crawford",
                "The Kin of Ata Are Waiting for You,|Dorothy Bryant",
                "The King in Yellow|Robert W. Chambers",
                "The King of Elfland's Daughter|Lord Dunsany",
                "King Rat|China Miéville",
                "Kingdoms of Elfin|Sylvia Townsend Warner",
                "The Kingdoms of Thorn and Bone|Greg Keyes",
                "Kingfisher|Patricia A. McKillip",
                "The Kingkiller Chronicle|Patrick Rothfuss",
                "Krondor's Sons (The Riftwar Stories)|Raymond E. Feist",
                "Kushiel's Legacy|Jacqueline Carey",
                "The Land Across|Gene Wolfe",
                "The Last Dragon|Silvana De Mari",
                "The Last Unicorn|Peter S. Beagle",
                "The Last Voyage of Somebody the Sailor|John Barth",
                "Latro|Gene Wolfe",
                "The Lays of Anuskaya|Bradley Beaulieu",
                "The Lays of Beleriand|J. R. R. Tolkien",
                "Legends of the Riftwar|Raymond E. Feist",
                "Legendborn|Tracy Deonn",
                "Letters from a Lost Uncle|Mervyn Peake",
                "The Library at Mount Char|Scott Hawkins",
                "The Lies of Locke Lamora|Scott Lynch",
                "Life of Pi|Yann Martel",
                "The Life and Opinions of the Tomcat Murr|E. T. A. Hoffmann",
                "Lilith|George MacDonald",
                "The Little Grey Men|BB",
                "The Little White Horse|Elizabeth Goudge",
                "Little People|Tom Holt",
                "Little, Big|John Crowley",
                "Lolly Willowes|Sylvia Townsend Warner",
                "Long Black Curl|Alex Bledsoe",
                "The Long Look|Richard Parks",
                "The Long Price Quartet|Daniel Abraham",
                "The Lord of the Rings|J. R. R. Tolkien",
                "The Lost Continent: The Story of Atlantis|C. J. Cutcliffe Hyne",
                "Lost Tales|J. R. R. Tolkien",
                "Lud-in-the-Mist|Hope Mirrlees",
                "Lyonesse|Jack Vance",
                "Lyra|Patricia Wrede",
                "M is for Magic|Neil Gaiman",
                "The Magic City|E. Nesbit",
                "The Magician Out of Manchuria|Charles G. Finney",
                "The Magician Trilogy|Jenny Nimmo",
                "The Magicians|Lev Grossman",
                "Magyk|Angie Sage",
                "Malazan Book of the Fallen|Steven Erikson",
                "The Malloreon|David and Leigh Eddings",
                "The Man Who Was Thursday|G. K. Chesterton",
                "Mandricardo|Lin Carter",
                "Marianne|Sheri S. Tepper",
                "The Mark of the Demons|John Jakes",
                "Martin Dressler|Steven Millhauser",
                "Mary Poppins|P. L. Travers",
                "The Mask of the Sorcerer|Darrell Schweitzer",
                "The Master and Margarita|Mikhail Bulgakov",
                "Master Li|Barry Hughart",
                "Matilda|Roald Dahl",
                "May Bird and the Ever After|Jodie Lynn Anderson",
                "Mention My Name in Atlantis|John Jakes",
                "Merlin's Ring|H. Warner Munn",
                "The Merman's Children|Poul Anderson",
                "Mickelsson's Ghosts|John Gardner",
                "Millroy the Magician|Paul Theroux",
                "Mistress Masham's Repose|T. H. White",
                "Mistborn|Brandon Sanderson",
                "The Mists of Avalon|Marion Zimmer Bradley",
                "Memory, Sorrow, and Thorn|Tad Williams",
                "Moonheart|Charles de Lint",
                "The Mortal Instruments|Cassandra Clare",
                "Mr. Magorium's Wonder Emporium|Suzanne Weyn",
                "Mr. Pye|Mervyn Peake",
                "The Murders of Molly Southbourne|Tade Thompson",
                "Myth Adventures|Robert Asprin",
                "The Nightrunner|Lynn Flewelling",
                "The Name of the Wind|Patrick Rothfuss",
                "The Chronicles of Narnia|C.S. Lewis",
                "The Neverending Story|Michael Ende",
                "Neverwhere|Neil Gaiman",
                "A Night in the Lonesome October|Roger Zelazny",
                "The Night Land|William Hope Hodgson",
                "Nights at the Circus|Angela Carter",
                "Nothing But Blue Skies|Tom Holt",
                "Number9Dream|David Mitchell",
                "The Named|Marianne Curley",
                "The Obernewtyn Chronicles|Isobelle Carmody",
                "Od Magic|Patricia McKillip",
                "Oksa Pollock|Anne Plichota and Cendrine Wolf",
                "Ombria in Shadow|Patricia McKillip",
                "On Stranger Tides|Tim Powers",
                "The Once and Future King|T.H. White",
                "Orlando|Virginia Woolf",
                "Our Ancestors a set|Italo Calvino",
                "Overtime|Tom Holt",
                "Paint Your Dragon|Tom Holt",
                "The Paladin|C. J. Cherryh",
                "Pandava Quintet|Roshani Chokshi",
                "Passing Strange|Ellen Klages",
                "Peace|Gene Wolfe",
                "Pellinor|Alison Croggon",
                "Pellucidar|Edgar Rice Burroughs",
                "The Pendragon Adventure|D. J. MacHale",
                "Percy Jackson & The Olympians|Rick Riordan",
                "The Perilous Gard|Elizabeth Marie Pope",
                "Peter and Wendy aka Peter Pan|J. M. Barrie",
                "Peter Pan in Kensington Gardens|J. M. Barrie",
                "Phantastes|George MacDonald",
                "Pilgermann|Russell Hoban",
                "Pinocchio|Carlo Collodi",
                "The Piratica|Tanith Lee",
                "The Place of the Lion|Charles Williams",
                "Policeman Bluejay|L. Frank Baum",
                "The Poppy War|R. F. Kuang",
                "Portrait of Jennie|Robert Nathan",
                "The Power of Five Series (a.k.a. The Gatekeepers Series)|Anthony Horowitz",
                "Practical Magic|Alice Hoffman",
                "Prince of Nothing trilogy|R. Scott Bakker",
                "The Princes of the Golden Cage|Nathalie Mallet",
                "The Princess Bride|William Goldman",
                "Promise of Blood|Brian McClellan[3]",
                "Protector of the Small|Tamora Pierce",
                "The Chronicles of Prydain|Lloyd Alexander",
                "The Quest of Kadji|Lin Carter",
                "Quidditch Through The Ages|J.K. Rowling",
                "The Quentaris Chronicles|various Australian writers",
                "Queste|Angie Sage",
                "The Rage of Dragons|Evan Winter",
                "Ranger's Apprentice|John Flanagan",
                "The Raven Cycle|Maggie Stiefvater",
                "The Raven Tower|Ann Leckie",
                "Raybearer duology|Jordan Ifueko",
                "Red Moon and Black Mountain|Joy Chant",
                "Red Sister|Mark Lawrence",
                "The Red Threads of Fortune|JY Yang",
                "Redwall|Brian Jacques",
                "The Revenants|Sheri S. Tepper",
                "A Riddle of Roses|Caryl Cude Mullin",
                "The Riddle-Master trilogy|Patricia A. McKillip",
                "Riftwar|Raymond E. Feist",
                "Rose Daughter|Robin McKinley",
                "Roverandom|J. R. R. Tolkien",
                "The Runelords|David Farland",
                "Rusalka|C. J. Cherryh",
                "Sangreal Trilogy|Jan Siegel",
                "The Scarlet Fig|Avram Davidson",
                "The School for Good and Evil|Soman Chainani",
                "The Sea of Trolls|Nancy Farmer",
                "The Secrets of the Immortal Nicholas Flamel|Michael Scott",
                "Senlin Ascends|Josiah Bancroft",
                "Septimus Heap|Angie Sage",
                "The Shadow Campaigns|Django Wexler",
                "The Shadow of What Was Lost|James Islington",
                "Shadow Warrior|Chris Bunch",
                "Shadowmarch|Tad Williams",
                "Shadowplay|Tad Williams",
                "Shannara|Terry Brooks",
                "The Shape-Changer's Wife|Sharon Shinn",
                "The Shapeshifter|Ali Sparkes",
                "Shardik|Richard Adams",
                "The Shattered Goddess|Darrell Schweitzer",
                "Shattered Sea|Joe Abercrombie",
                "The Shaving of Shagpat|George Meredith",
                "Shrek!|William Steig",
                "The Silent Land|Graham Joyce",
                "The Silmarillion|J. R. R. Tolkien",
                "Silver John|Manly Wade Wellman",
                "Silverlock|John Myers Myers",
                "Skin of the Sea|Natasha Bowen",
                "Snow White and Rose Red|Patricia C. Wrede",
                "Snow White and the Seven Samurai|Tom Holt",
                "Solstice Wood|Patricia A. McKillip",
                "Some Kind of Fairy Tale|Graham Joyce",
                "Something Rich and Strange|Patricia A. McKillip",
                "Song for the Basilisk|Patricia A. McKillip",
                "A Song of Ice and Fire|George R. R. Martin",
                "A Song of Wraiths and Ruin|Roseanne A. Brown",
                "The Song of the Lioness|Tamora Pierce",
                "The Song of the Shattered Sands|Bradley Beaulieu",
                "The Sorcerer's Ship|Hannes Bok",
                "The Sorceress and the Cygnet|Patricia A. McKillip",
                "The Spiderwick Chronicles|Holly Black",
                "Spindle's End|Robin McKinley",
                "The Stand|Stephen King",
                "The Starcatchers|Dave Barry and Ridley Pearson",
                "Stardust|Neil Gaiman",
                "The Stardust Thief|Chelsea Abdullah",
                "The Steerswoman|Rosemary Kirstein",
                "Stone and Sky trilogy|Graham Edwards",
                "Stone of Farewell|Tad Williams",
                "The Stormlight Archive|Brandon Sanderson",
                "Stravaganza|Mary Hoffman",
                "The Stress of Her Regard|Tim Powers",
                "Stuart Little|E. B. White",
                "The Sundering Flood|William Morris",
                "The Switchers Trilogy|Kate Thompson",
                "The Sword of Truth|Terry Goodkind",
                "The Sword Smith|Eleanor Arnason",
                "Swordbird|Nancy Yi Fan",
                "Swords Against the Shadowland|Robin Wayne Bailey",
                "The Swords of Lankhmar|Fritz Leiber",
                "Swordspoint|Ellen Kushner",
                "Symphony of Ages|Elizabeth Haydon",
                "Tailchaser's Song|Tad Williams",
                "Tales From The Flat Earth|Tanith Lee",
                "The Tales of Alvin Maker|Orson Scott Card",
                "The Tales Of Beedle The Bard|J.K. Rowling",
                "The Kane Chronicles|Rick Riordan",
                "The Tales of the Otori|Lian Hearn",
                "Talking Man|Terry Bisson",
                "Tam Lin|Pamela Dean",
                "The Tamuli|David Eddings",
                "Tara of the Twilight|Lin Carter",
                "Tarzan|Edgar Rice Burroughs",
                "A Taste of Honey|Kai Ashante Wilson",
                "Ten Silver Coins|Andrew Kooman",
                "These Violent Delights|Chloe Gong",
                "The Thief of Always|Clive Barker",
                "The Thief Lord|Cornelia Funke",
                "Thieves' World series edited|Robert Asprin and Lynn Abbey",
                "The Third Policeman|Flann O'Brien",
                "Thongor Against the Gods|Lin Carter",
                "Thongor at the End of Time|Lin Carter",
                "Thongor Fights the Pirates of Tarakus|Lin Carter",
                "Thongor in the City of Magicians|Lin Carter",
                "Thongor of Lemuria|Lin Carter",
                "The Three Impostors|Arthur Machen",
                "The Three Worlds Cycle|Ian Irvine",
                "The Chronicles of Thomas Covenant|Stephen R. Donaldson",
                "Three Hearts and Three Lions|Poul Anderson",
                "Three to See the King|Magnus Mills",
                "The Throme of the Erril of Sherill|Patricia A. McKillip",
                "Through the Looking-Glass|Lewis Carroll",
                "Thunder on the Left|Christopher Morley",
                "Tigana|Guy Gavriel Kay",
                "Tithe: A Modern Faerie Tale|Holly Black",
                "Titus series (aka Gormenghast)|Mervyn Peake",
                "To Green Angel Tower|Tad Williams",
                "Tomoe Gozen|Jessica Amanda Salmonson",
                "The Tooth Fairy|Graham Joyce",
                "Topper duo|Thorne Smith",
                "The Touch of Evil|John Rackham",
                "The Tower at Stony Wood|Patricia McKillip",
                "Traitor's Blade|Sebastien de Castell",
                "Traitor Son Cycle|Miles Cameron",
                "The Traitor Baru Cormorant|Seth Dickinson",
                "The Tree of Swords and Jewels|C. J. Cherryh",
                "The Twilight|Stephenie Meyer",
                "The Tea Master and the Detective|Aliette de Bodard",
                "Un Lun Dun|China Miéville",
                "The Underland Chronicles|Suzanne Collins",
                "Unfinished Tales|J. R. R. Tolkien",
                "The Unicorn|Tanith Lee",
                "The Unicorns of Balinor|Mary Stanton",
                "The Unspoken Name|A.K. Larkwood",
                "Vampire Academy|Richelle Mead",
                "Valhalla|Tom Holt",
                "Velgarth|Mercedes Lackey",
                "Villains|Necessity|Eve Forward",
                "Viriconium cycle|M. John Harrison",
                "The Vlad Taltos books|Steven Brust",
                "Von Bek|Michael Moorcock",
                "A Voyage to Arcturus|David Lindsay",
                "The Wardstone Chronicles|Joseph Delaney",
                "War in Heaven|Charles Williams",
                "The War of Dreams (aka The Infernal Desire Machines of Doctor Hoffman)|Angela Carter",
                "The War of the Flowers|Tad Williams",
                "Warbreaker|Brandon Sanderson",
                "The Warrior of World's End|Lin Carter",
                "Warriors|Erin Hunter",
                "The Water-Babies, A Fairy Tale for a Land Baby|Charles Kingsley",
                "The Water of the Wondrous Isles|William Morris",
                "Watership Down|Richard Adams",
                "Weaveworld|Clive Barker",
                "The Well at the World's End|William Morris",
                "The Well of the Unicorn|Fletcher Pratt",
                "Wheel of Time|Robert Jordan",
                "When the Birds Fly South|Stanton A. Coblentz",
                "When the Idols Walked|John Jakes",
                "The Whitby Witches|Robin Jarvis",
                "The White Isle|Darrell Schweitzer",
                "Who's Afraid of Beowulf?|Tom Holt",
                "Who Censored Roger Rabbit?|Gary K. Wolf",
                "The Wind in the Willows|Kenneth Grahame",
                "''Wings of Fire|Tui T. Sutherland",
                "Winnie-The-Pooh|A.A. Milne",
                "Winter Rose|Patricia McKillip",
                "Winter's Tale|Mark Helprin",
                "Winternight trilogy|Katherine Arden",
                "Wish You Were Here|Tom Holt",
                "Wisp of a Thing|Alex Bledsoe",
                "Witch of the Four Winds|John Jakes",
                "The Witcher|Andrzej Sapkowski",
                "The Witness for the Dead|Katherine Addison",
                "The Wish Giver|Bill Brittain",
                "The Wizard of Lemuria|Lin Carter",
                "Wizard of the Pigeons|Megan Lindholm",
                "The Wizard of Zao|Lin Carter",
                "The Wolf Leader|Alexandre Dumas",
                "The Wolves in the Walls|Neil Gaiman",
                "Women of the Otherworld|Canadian author Kelley Armstrong.",
                "The Wonderful Wizard of Oz|Frank L. Baum.",
                "The Wood Beyond the World|William Morris",
                "The World According to Novikoff|Andrei Gusev",
                "The Worldbreaker|Kameron Hurley",
                "The Worm Ouroboros|E. R. Eddison",
                "A Wrinkle in Time|Madeleine L'Engle",
                "Wizard and Glass|Stephen King",
                "Xanth|Piers Anthony",
                "Yamada Monogatari: The Emperor in Shadow|Richard Parks",
                "Yamada Monogatari: The War God's Son|Richard Parks",
                "Yamada Monogatari: To Break the Demon Gate|Richard Parks",
                "Young Wizards|Diane Duane",
                "Yvgenie|C. J. Cherryh",
                "Zimiamvia|E. R. Eddison",
                "Zoo City|Lauren Beukes"
            };

            foreach (var book in books)
            {
                _bookManager.CreateBook(new Book
                {
                    Author = book.Split('|')[1],
                    Title = book.Split('|')[0],
                    NumberOfLicences = new Random().Next(1, 5),
                    YearOfPublication = new Random().Next(1950, 2022),
                    NumberOfPages = new Random().Next(100, 500)
                });
            }

            for (int i = 1; i <= 5; i++)
            {
                _userManager.CreateUser(i <= 2, new User
                {
                    AccountState = AccountState.Active,
                    Firstname = "Firstname",
                    Surname = "Surname",
                    Username = $"Username{i}",
                    Password = "123",
                    Address = new Address
                    {
                        Zip = "67801",
                        City = "City",
                        Street = "Street",
                        DescriptiveNumber = $"12{i}",
                        OrientationNumber = $"{i}"
                    },
                    Pin = (101010008 + (11 * i)).ToString("D10"),
                });
            }
            
            var bookToBorrow1 = _bookManager.Find(BusinessLayer.Managers.FindType.AND, "Hobbit", "Tolkien").First();
            var user1 = _adminManager.Find(BusinessLayer.Managers.FindType.AND, "Username1").First();
            _userManager.BorrowBook(user1._id, bookToBorrow1._id);
            var bookToBorrow2 = _bookManager.Find(BusinessLayer.Managers.FindType.AND, "Lor", "Tolkien").First();
            _userManager.BorrowBook(user1._id, bookToBorrow2._id);
            _userManager.ReturnBook(user1._id, bookToBorrow2._id);

            var bookToBorrow3 = _bookManager.Find(BusinessLayer.Managers.FindType.AND, "Sil", "Tolkien").First();
            var user2 = _adminManager.Find(BusinessLayer.Managers.FindType.AND, "Username2").First();
            _userManager.BorrowBook(user2._id, bookToBorrow3._id);
        }
    }
}
