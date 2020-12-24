using System;
using System.Collections.Generic;


namespace BrickWork
{
    class Program
    {
        static void Main(string[] args)
        {
            //Headline
            Console.WriteLine("BrickWork");
            Console.WriteLine();

            //Get the size of the wall layers
            Console.WriteLine("Please enter the size of the brick wall: ");

            // call the size input validation method
            // save the validated input in new instance of the Brickwall
            BrickWall bw = new BrickWall(SizeInput());


            //enter layer 1:
            Console.WriteLine("Please enter the layout of the first layer of the wall: ");
            // Get and validate the user input.
            BricksInput(bw.Layer1);


            //Call the solution method and save the result in layer2 field of BrickWall instance.
            Console.WriteLine("\noutput: ");
            bw.Layer2 = BuildOverLayer(bw.Layer1);

            // Output:
            // validate the new layer and print if sollution exists.
            ValidateLayer(bw.Layer2);

            Console.WriteLine("Press any key to exit..");
            Console.ReadKey();


            //methods:

            // validate and gather user's size input:
            static int[,] SizeInput()
            {
                string errMsg = "\nArea size must be two even numbers separated by space. " +
                    "\nBoth numbers must be less than 100. " +
                    "Negative values or zero are not acceptable." +
                    "\n\nPlease try again: \n";
                string input = "";
                string buffer = "";
                bool isValid = false;
                int spaces = 0;
                int n = 0;
                int m = 0;

                //Input n and m for the layer:
                //Break the loop only if the data is in correct form
                do
                {
                    try
                    {
                        // reset the values of variables for this iteration
                        isValid = true;
                        n = 0;
                        m = 0;
                        spaces = 0;
                        buffer = "";
                        // get the input from console and add it to the input string
                        input = Console.ReadLine();

                        //loop through the user input, and if there is space switch variable
                        for (int i = 0; i < input.Length; i++)
                        {
                            //Buffer variable appends every char from the string.
                            buffer += input[i];

                            // if the char is space 
                            if (spaces == 1)
                            {
                                // Convert to int and write M
                                m = int.Parse(buffer);
                            }
                            // if there is space write the appended data, and reset the buffer.
                            if (input[i] == ' ')
                            {
                                //Convert to int and Write N
                                n = int.Parse(buffer);
                                buffer = "";
                                spaces++;
                            }
                        }

                        // validate the input for N and M
                        // if not valid throw error to be catched

                        // no negative numbers or zero 
                        if (n <= 0 || m <= 0)
                        {
                            throw new OverflowException();
                        }
                        // check the range (1 - 100) for N
                        if (n % 2 != 0 || n > 98)
                        {
                            throw new FormatException();
                        }
                        // check the range (1 - 100) for M
                        if (m % 2 != 0 || m > 98)
                        {
                            throw new FormatException();
                        }
                    }

                    catch (FormatException)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine(errMsg);
                        Console.ForegroundColor = ConsoleColor.Gray;
                        isValid = false;

                    }
                    catch (OverflowException)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine(errMsg);
                        Console.ForegroundColor = ConsoleColor.Gray;
                        isValid = false;
                    }
                } while (!isValid);

                // If validation is successful create the  first layer.
                int[,] layer = new int[n, m];

                //Information about the Wall and first Layer:
                //Console.ForegroundColor = ConsoleColor.Green;
                //Console.WriteLine("\nStatistic:");
                //Console.WriteLine("============");
                //Console.WriteLine("\nUser input:             {0}" +
                //                  "\nSpaces:                 {1}" +
                //                  "\nN:                      {2}" +
                //                  "\nM:                      {3}" +
                //                  "\nBuffer:                 {4}", input, spaces, n, m, buffer);
                //ShowArrayInfo(layer);
                //Console.WriteLine("first Layer blueprint::");
                //Console.WriteLine(PrintLayer(layer));
                //Console.ForegroundColor = ConsoleColor.Gray;
                //stats

                // return the validated data.
                return layer;
            }

            // Get the user's input for the layout of the first layer of bricks
            static void BricksInput(int[,] layer)
            {
                // pass the size of the wall to local variables for better readability.
                int row = layer.GetLength(0);
                int col = layer.GetLength(1);
                string errMsg = string.Format("\nYour wall has {0} rows and {1} columns.", row, col) +
                   "\nEvery brick consist of two identical numbers separated by space. " +
                   "\nEach number starts from 1 to the end dimentions of the wall." +
                   "\n\nPlease try again: \n";
                //in the dictionary we will save the hash
                var dictionary = new Dictionary<int, int>();
                bool isValid = false;
                int[] numbers = null;
                string[] input;
                //Input for the bricks layout:
                do
                {
                    try
                    {
                        isValid = true;

                        for (int i = 0; i < row; i++)
                        {
                            //read each row of the console and past it to the array of numbers
                            input = Console.ReadLine().Split(' ');

                            //init new array with input lenght.
                            numbers = new int[input.Length];

                            for (int j = 0; j < col; j++)
                            {
                                //validate each row length
                                if (numbers.Length != col)
                                {
                                    throw new FormatException();
                                }
                                //convert and transfer each row of the layer
                                numbers[j] = Convert.ToInt32(input[j]);
                                layer[i, j] = numbers[j];
                            }

                        }
                        //Bricks validation:
                        // reset the dictionary for this iteration
                        dictionary.Clear();
                        foreach (int n in layer)
                        {
                            //add keys to dict to keep track of occurrences and later validation.
                            if (dictionary.ContainsKey(n))
                            {
                                //increase the value of key if detected
                                dictionary[n]++;
                            }
                            else dictionary[n] = 1;
                        }

                        //validate if there is a brick on 3 rows/cols
                        foreach (var element in dictionary)
                        {
                            // if the value of the key in the dictionary is more
                            if (element.Value > 2)
                            {
                                throw new Exception();
                            }
                        }

                    }

                    catch (FormatException)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine(errMsg);
                        Console.ForegroundColor = ConsoleColor.Gray;
                        isValid = false;

                    }
                    catch (OverflowException)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine(errMsg);
                        Console.ForegroundColor = ConsoleColor.Gray;
                        isValid = false;
                    }
                    catch (IndexOutOfRangeException)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine(errMsg);
                        Console.ForegroundColor = ConsoleColor.Gray;
                        isValid = false;

                    }
                    catch (Exception)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Bricks cannot span on more than 2 slots per rows/columns");
                        Console.WriteLine("Please try again: \n");
                        Console.ForegroundColor = ConsoleColor.Gray;
                        isValid = false;
                    }
                } while (!isValid);
            }

            //solution:
            static int[,] BuildOverLayer(int[,] layer1)
            {
                int row = layer1.GetLength(0);
                int col = layer1.GetLength(1);
                // init a 2d array with same size as layer1 to be filled with the new bricks
                int[,] topLayer = new int[row, col];
                // new bricks will be added with unique id
                int brickId = 1;


                /*
                  Make the top layer: check the brick orientation, and follow this logical structure:
                  also we are checking only the odd rows and adding brick on the row below.
                          if | --> =
                          if = --> | =

                 example:

                          1 1 2 2  ==> 1 2 2 3
                          3 3 4 4  ==> 1 4 4 3

                 symbols:
                          |   horizontal brick / brick separator
                          =   two vertical bricks;
                          --> therefore (place bricks)
                          *   current element in iteration
                         [ ]  newly added element
                 */

                // loop through the input layer1
                for (int i = 0; i < row - 1; i++)
                {
                    //only odd row:
                    if (i % 2 == 0)
                    {
                        for (int j = 0; j < col - 1; j++)
                        {
                            //check if current element of the layer1 is begining of horizontal brick:

                            //first half of horizontal brick.
                            if (layer1[i, j] == layer1[i, j + 1])
                            {
                                //Console.WriteLine(" first half of horizontal brick;");
                                // check if there is already a brick in the top layer
                                if(topLayer[i, j] != 0)
                                {
                                    // add only bottom row:
                                    topLayer[i + 1, j] = brickId;

                                    //example view of topLayer:
                                    // 1
                                    //[2]
                                }
                                // if there are no bricks in the top layer, place one horizontal.
                                if (topLayer[i, j] == 0)
                                {
                                    topLayer[i, j] = brickId;
                                    topLayer[i + 1, j] = brickId;

                                    //example view of topLayer:
                                    // [1] -
                                    // [1] -
                                }

                                // Determine  if we are close to last iteration 
                                if (j == col - 2)
                                {
                                    //Console.WriteLine("End - place vertical brick");
                                    topLayer[i, j + 1] = brickId + 1;
                                    topLayer[i + 1, j + 1] = brickId + 1;

                                    //example view of topLayer:
                                    // 1*|[2]|
                                    // 1 |[2]|

                                }

                                
                                brickId++;

                            }
                            //check if current element of layer is the end half. 
                            // check next elements:
                            //

                            if (layer1[i, j] != layer1[i, j + 1] && layer1[i, j] != layer1[i + 1, j])
                            {
                                //check if next brick is vertical:
                                if (layer1[i, j + 1] == layer1[i + 1, j + 1])
                                {
                                    //Console.WriteLine("next brick is |");
                                    topLayer[i, j] = brickId;
                                    topLayer[i + 1, j] = brickId;
                                    //new
                                    topLayer[i, j + 1] = brickId + 1;
                                    // 1|[2*]| [3] -
                                    // 1|[2] |

                                    //but if we are on the las operation.
                                    if (j == col - 2)
                                    {
                                        // Console.WriteLine("End  on | brick");
                                        topLayer[i, j] = brickId;
                                        topLayer[i + 1, j] = brickId + 1;
                                        
                                        topLayer[i, j + 1] = brickId;
                                        topLayer[i + 1, j + 1] = brickId + 1;
                                        // 1|[2*]| [2]
                                        // 1|[3] | [3]


                                    }
                                }
                                // check if next brick is horizontal:
                                if (layer1[i, j + 1] != layer1[i + 1, j  +1])
                                {
                                    //Console.WriteLine("next brick is =");
                                    topLayer[i, j] = brickId;
                                    topLayer[i, j + 1] = brickId;
                                    topLayer[i + 1, j] = brickId + 1;

                                    //complete first brick:
                                    //example view of topLayer:
                                    // 1 |[2*][2] |
                                    // 1 |[3 ]  - |

                                }


                                brickId++;
                            }

                            // if we are on vertical brick:
                            if (layer1[i, j] == layer1[i + 1, j])
                            {
                                //Console.WriteLine("On vertical brick");
                                // if the next brick is also vertical:
                             
                                    topLayer[i, j] = brickId;
                                    topLayer[i, j + 1] = brickId;
                                    topLayer[i + 1, j] = brickId + 1;
                                    //complete first brick:
                                    //example view of topLayer:
                                    // 1 |[2*][2] |
                                    // 1 |[3 ]  - |
                                

                                // if we are on the final operation:
                                if (j == col - 2)
                                {
                                    topLayer[i + 1, j + 1] = brickId + 1;
                          
                                    //complete the two horizontal bricks:
                                    //example view of topLayer:
                                    // 1 |[2*][2] |
                                    // 1 |[3 ][3] |

                                }

                                brickId++;

                            }

                        }

                    }
                }

                // return the solved new layer
                return topLayer;
                
            }
            

            // Validate the new top layer and print it if the sollutin is valid.
            static void ValidateLayer(int[,] layer)
            {

                var dictionary = new Dictionary<int, int>();
                bool isValid = true;
                
                foreach (int n in layer)
                {
                    //add keys to dict to keep track of occurrences and later validation.
                    if (dictionary.ContainsKey(n))
                    {
                        //increase the value of key if detected
                        dictionary[n]++;
                    }
                    else dictionary[n] = 1;

                    if(n <= 0)
                    {
                        isValid = false;
                    }
                }

                //validate if there is a brick on 3 rows/cols
                foreach (var element in dictionary)
                {
                    // if the value of the key in the dictionary is more
                    if (element.Value > 2)
                    {
                        isValid = false;
                    }
                }

                if (isValid)
                {
                    Console.WriteLine(PrintLayer(layer));
                }
                else Console.WriteLine( "-1 " + "\nNo solution exists!");
            }


            

            //Printlayers
            static string PrintLayer(int[,] layer)
            {
                int row = layer.GetLength(0);
                int col = layer.GetLength(1);
                string output = "";
                

                for (int i = 0; i < row; i++)
                {
                    for (int j = 0; j < col; j++)
                    { 
                       
                        output += string.Format("{0} ", layer[i, j]);

                    }

                    output += Environment.NewLine;
                }
                return output;
            }


            // utility method to display 2d array info, dimensions and length.
            static void ShowArrayInfo(Array arr)
            {
                Console.WriteLine("Length of Array:      {0,3}", arr.Length);
                Console.WriteLine("Number of Dimensions: {0,3}", arr.Rank);
       
                if (arr.Rank > 1)
                {
                    for (int dimension = 1; dimension <= arr.Rank; dimension++)
                        Console.WriteLine("   Dimension {0}: {1,3}", dimension,
                                          arr.GetUpperBound(dimension - 1) + 1);
                }
                Console.WriteLine();
            }

        }
    }
}
