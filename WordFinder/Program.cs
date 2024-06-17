int size = 6; // Tamaño de la matriz
var wordsToFind = new List<string> { "chill", "cold", "wind" };

var wordFinder = new WordFinder.WordFinder(size, wordsToFind);
wordFinder.PrintMatrix();

var foundWords = wordFinder.Find(wordsToFind);

Console.WriteLine("Palabras encontradas:");
foreach (var word in foundWords)
{
    Console.WriteLine(word);
}
