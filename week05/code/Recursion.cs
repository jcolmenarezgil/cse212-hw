using System.Collections;

public static class Recursion
{
    /// <summary>
    /// #############
    /// # Problem 1 #
    /// #############
    /// Using recursion, find the sum of 1^2 + 2^2 + 3^2 + ... + n^2
    /// and return it.  Remember to both express the solution 
    /// in terms of recursive call on a smaller problem and 
    /// to identify a base case (terminating case).  If the value of
    /// n <= 0, just return 0.   A loop should not be used.
    /// </summary>
    public static int SumSquaresRecursive(int n)
    {
        // 1. Caso Base (Salida de emergencia): 
        // Si n llega a 0 o menos, el anillo se cierra y retornamos 0.
        if (n <= 0)
        {
            return 0;
        }

        // 2. Paso Recursivo:
        // Calculamos el cuadrado del nodo actual (n * n) 
        // y le sumamos el resultado de la "cebolla más pequeña" (n - 1).
        return (n * n) + SumSquaresRecursive(n - 1);
    }

    /// <summary>
    /// #############
    /// # Problem 2 #
    /// #############
    /// Using recursion, insert permutations of length
    /// 'size' from a list of 'letters' into the results list.  This function
    /// should assume that each letter is unique (i.e. the 
    /// function does not need to find unique permutations).
    ///
    /// In mathematics, we can calculate the number of permutations
    /// using the formula: len(letters)! / (len(letters) - size)!
    ///
    /// For example, if letters was [A,B,C] and size was 2 then
    /// the following would the contents of the results array after the function ran: AB, AC, BA, BC, CA, CB (might be in 
    /// a different order).
    ///
    /// You can assume that the size specified is always valid (between 1 
    /// and the length of the letters list).
    /// </summary>
    public static void PermutationsChoose(List<string> results, string letters, int size, string word = "")
    {
        // 1. Caso Base: 
        // Si la palabra que estamos construyendo alcanzó el tamaño deseado, 
        // la agregamos a los resultados.
        if (word.Length == size)
        {
            results.Add(word);
            return;
        }

        // 2. Paso Recursivo:
        // Iteramos sobre las letras disponibles para elegir la siguiente.
        for (int i = 0; i < letters.Length; i++)
        {
            // Extraemos la letra elegida para que no se repita en esta rama
            char chosenLetter = letters[i];
            string remainingLetters = letters.Remove(i, 1);

            // Llamada recursiva con el subproblema:
            // - Pasamos la palabra con la nueva letra agregada
            // - Pasamos el resto de las letras que quedan disponibles
            PermutationsChoose(results, remainingLetters, size, word + chosenLetter);
        }
    }

    /// <summary>
    /// #############
    /// # Problem 3 #
    /// #############
    /// Imagine that there was a staircase with 's' stairs.  
    /// We want to count how many ways there are to climb 
    /// the stairs.  If the person could only climb one 
    /// stair at a time, then the total would be just one.  
    /// However, if the person could choose to climb either 
    /// one, two, or three stairs at a time (in any order), 
    /// then the total possibilities become much more 
    /// complicated.  If there were just three stairs,
    /// the possible ways to climb would be four as follows:
    ///
    ///     1 step, 1 step, 1 step
    ///     1 step, 2 step
    ///     2 step, 1 step
    ///     3 step
    ///
    /// With just one step to go, the ways to get
    /// to the top of 's' stairs is to either:
    ///
    /// - take a single step from the second to last step, 
    /// - take a double step from the third to last step, 
    /// - take a triple step from the fourth to last step
    ///
    /// We don't need to think about scenarios like taking two 
    /// single steps from the third to last step because this
    /// is already part of the first scenario (taking a single
    /// step from the second to last step).
    ///
    /// These final leaps give us a sum:
    ///
    /// CountWaysToClimb(s) = CountWaysToClimb(s-1) + 
    ///                       CountWaysToClimb(s-2) +
    ///                       CountWaysToClimb(s-3)
    ///
    /// To run this function for larger values of 's', you will need
    /// to update this function to use memoization.  The parameter
    /// 'remember' has already been added as an input parameter to 
    /// the function for you to complete this task.
    /// </summary>
    public static decimal CountWaysToClimb(int s, Dictionary<int, decimal>? remember = null)
    {
        // Inicializamos el diccionario si es la primera llamada (el Big Bang del proceso)
        remember ??= new Dictionary<int, decimal>();

        // 1. Casos Base (Las salidas de emergencia conocidas)
        if (s <= 0) return 0;
        if (s == 1) return 1;
        if (s == 2) return 2;
        if (s == 3) return 4;

        // 2. ¿Ya conocemos la respuesta para este número de escalones?
        // Si está en el "mapa", lo devolvemos de inmediato. O(1)
        if (remember.ContainsKey(s))
        {
            return remember[s];
        }

        // 3. Paso Recursivo con "Memoria":
        // Calculamos la suma de las tres posibilidades de salto.
        // Pasamos el diccionario 'remember' en cada llamada para que todos compartan la memoria.
        decimal ways = CountWaysToClimb(s - 1, remember) +
                       CountWaysToClimb(s - 2, remember) +
                       CountWaysToClimb(s - 3, remember);

        // 4. Guardamos el resultado antes de devolverlo para futuras consultas.
        remember[s] = ways;

        return ways;
    }

    /// <summary>
    /// #############
    /// # Problem 4 #
    /// #############
    /// A binary string is a string consisting of just 1's and 0's.  For example, 1010111 is 
    /// a binary string.  If we introduce a wildcard symbol * into the string, we can say that 
    /// this is now a pattern for multiple binary strings.  For example, 101*1 could be used 
    /// to represent 10101 and 10111.  A pattern can have more than one * wildcard.  For example, 
    /// 1**1 would result in 4 different binary strings: 1001, 1011, 1101, and 1111.
    ///	
    /// Using recursion, insert all possible binary strings for a given pattern into the results list.  You might find 
    /// some of the string functions like IndexOf and [..X] / [X..] to be useful in solving this problem.
    /// </summary>
    public static void WildcardBinary(string pattern, List<string> results)
    {
        // 1. Buscamos la posición del primer comodín en el patrón actual.
        int index = pattern.IndexOf('*');

        // 2. Caso Base: 
        // Si no hay más '*', el patrón es ya una cadena binaria pura.
        // La agregamos a los resultados y detenemos la recursión en esta rama.
        if (index == -1)
        {
            results.Add(pattern);
            return;
        }

        // 3. Paso Recursivo:
        // "Dividimos" el patrón en dos ramas. En ambas mantenemos lo que está 
        // antes del '*' y lo que está después, pero cambiamos el '*' por 0 y 1.

        // Rama del '0'
        string patternWithZero = pattern[..index] + "0" + pattern[(index + 1)..];
        WildcardBinary(patternWithZero, results);

        // Rama del '1'
        string patternWithOne = pattern[..index] + "1" + pattern[(index + 1)..];
        WildcardBinary(patternWithOne, results);
    }

    /// <summary>
    /// Use recursion to insert all paths that start at (0,0) and end at the
    /// 'end' square into the results list.
    /// </summary>
    public static void SolveMaze(List<string> results, Maze maze, int x = 0, int y = 0, List<ValueTuple<int, int>>? currPath = null)
    {
        // Inicialización del explorador
        if (currPath == null)
        {
            currPath = new List<ValueTuple<int, int>>();
        }

        // 1. CASOS BASE (Condiciones de parada)

        // A. ¿Es un movimiento válido? (Límites, paredes y no repetir nodos)
        if (!maze.IsValidMove(currPath, x, y))
        {
            return;
        }

        // 2. PASO RECURSIVO (Exploración)

        // Agregamos la posición actual al camino
        currPath.Add((x, y));

        // C. ¿Llegamos a la meta?
        if (maze.IsEnd(x, y))
        {
            results.Add(currPath.AsString());
        }
        else
        {
            // Si no es el fin, exploramos en las 4 direcciones cardinales
            // Norte, Sur, Este, Oeste
            SolveMaze(results, maze, x + 1, y, currPath); // Derecha
            SolveMaze(results, maze, x - 1, y, currPath); // Izquierda
            SolveMaze(results, maze, x, y + 1, currPath); // Abajo
            SolveMaze(results, maze, x, y - 1, currPath); // Arriba
        }

        // 3. EL BACKTRACK (La limpieza)
        // Importante: Al terminar de explorar todas las opciones desde (x, y),
        // debemos removernos del camino para que otras ramas puedan usar este espacio.
        currPath.RemoveAt(currPath.Count - 1);
    }
}