using System;
using System.Collections.Generic;

public class Network
{
    private readonly int _size;
    private readonly Dictionary<int, HashSet<int>> _connections;


    public Network(int size)
    {
        if (size <= 0)
            throw new ArgumentException("O número de elementos precisa ser um valor positivo");


        
        _size = size;
        _connections = new Dictionary<int, HashSet<int>>();
        


        for (int i = 1; i <= size; i++)
        {
            _connections[i] = new HashSet<int>();
        }
    }
    

    public void Connect(int element1, int element2)
    {
        if (!IsValidElement(element1) || !IsValidElement(element2))
            throw new ArgumentException("Os elementos precisam estar dentro do itervalo válido");
        
        if (element1 == element2)
            throw new ArgumentException("Não é permitido conectar um elemento a ele mesmo");

        _connections[element1].Add(element2);
        _connections[element2].Add(element1);
    }



    public bool Query(int element1, int element2)
    {
        if (!IsValidElement(element1) || !IsValidElement(element2))
            throw new ArgumentException("Os elementos precisam estar dentro do itervalo válido");

        if (element1 == element2)
            return true;

        return IsConnected(element1, element2, new HashSet<int>());
    }



    private bool IsConnected(int start, int target, HashSet<int> visited)
    {
        if (start == target) 
            return true;

        visited.Add(start);



        foreach (var neighbor in _connections[start])
        {
            if (!visited.Contains(neighbor) && IsConnected(neighbor, target, visited))
            {
                return true;
            }
        }
        

        return false;

        
    }



    private bool IsValidElement(int element)
    {
        return element > 0 && element <= _size;
    }
}



public class Program
{
    public static void Main()
    {
        try
        {
            Network network = new Network(8);

           
            network.Connect(1, 2);
            network.Connect(6, 2);
            network.Connect(2, 4);
            network.Connect(5, 8);

            
            Console.WriteLine($"1 e 6 estão conectados? {network.Query(1, 6)}"); 
            Console.WriteLine($"6 e 4 estão conectados? {network.Query(6, 4)}"); 
            Console.WriteLine($"7 e 4 estão conectados? {network.Query(7, 4)}"); 
            Console.WriteLine($"5 e 6 estão conectados? {network.Query(5, 6)}"); 
            Console.WriteLine($"5 e 8 estão conectados? {network.Query(5, 8)}"); 
            Console.WriteLine($"3 e 3 estão conectados? {network.Query(3, 3)}"); 
            Console.WriteLine(network.Query(9, 10));
        }
        catch (ArgumentException e)
        {
            Console.WriteLine($"Erro: {e.Message}");
        }
    }
}