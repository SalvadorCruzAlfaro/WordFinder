namespace WordFinder;

public class WordFinder
{
    private readonly char[,] _matrix;
    private readonly int _size;

    public WordFinder(int size, IEnumerable<string> words)
    {
        _size = size;
        _matrix = GenerateMatrix(size);
        InsertWords(words);
    }

    // Genera una matriz de letras aleatorias
    private char[,] GenerateMatrix(int size)
    {
        var random = new Random();
        var matrix = new char[size, size];

        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                matrix[i, j] = (char)random.Next('A', 'Z' + 1);
            }
        }

        return matrix;
    }

    // Inserta las palabras en la matriz
    private void InsertWords(IEnumerable<string> words)
    {
        foreach (var word in words)
        {
            bool placed = false;

            while (!placed)
            {
                var random = new Random();
                int row = random.Next(_size);
                int col = random.Next(_size);
                bool horizontal = random.Next(2) == 0;

                if (horizontal)
                {
                    if (col + word.Length <= _size)
                    {
                        for (int i = 0; i < word.Length; i++)
                        {
                            _matrix[row, col + i] = word[i];
                        }
                        placed = true;
                    }
                }
                else
                {
                    if (row + word.Length <= _size)
                    {
                        for (int i = 0; i < word.Length; i++)
                        {
                            _matrix[row + i, col] = word[i];
                        }
                        placed = true;
                    }
                }
            }
        }
    }

    // Encuentra las palabras en la matriz
    public IEnumerable<string> Find(IEnumerable<string> wordstream)
    {
        var foundWords = new List<string>();

        foreach (var word in wordstream)
        {
            if (SearchWord(word))
            {
                foundWords.Add(word);
            }
        }

        return foundWords;
    }

    private bool SearchWord(string word)
    {
        // Buscar horizontalmente
        for (int row = 0; row < _size; row++)
        {
            for (int col = 0; col <= _size - word.Length; col++)
            {
                if (CheckHorizontal(row, col, word))
                {
                    return true;
                }
            }
        }

        // Buscar verticalmente
        for (int col = 0; col < _size; col++)
        {
            for (int row = 0; row <= _size - word.Length; row++)
            {
                if (CheckVertical(row, col, word))
                {
                    return true;
                }
            }
        }

        return false;
    }

    private bool CheckHorizontal(int row, int col, string word)
    {
        for (int i = 0; i < word.Length; i++)
        {
            if (_matrix[row, col + i] != word[i])
            {
                return false;
            }
        }
        return true;
    }

    private bool CheckVertical(int row, int col, string word)
    {
        for (int i = 0; i < word.Length; i++)
        {
            if (_matrix[row + i, col] != word[i])
            {
                return false;
            }
        }
        return true;
    }

    // Método para imprimir la matriz (opcional)
    public void PrintMatrix()
    {
        for (int i = 0; i < _size; i++)
        {
            for (int j = 0; j < _size; j++)
            {
                Console.Write(_matrix[i, j] + " ");
            }
            Console.WriteLine();
        }
    }
}
