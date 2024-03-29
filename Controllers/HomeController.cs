﻿using Newtonsoft.Json;
using Polard_Banknote.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Polard_Banknote.Controllers
{
    public class HomeController : Controller
    {


        public ActionResult UserInput()
        {
            List<Option> options = new List<Option>();
            options.Add(new Option() { Type = "Integer" });
            options.Add(new Option() { Type = "Float" });
            options.Add(new Option() { Type = "Hex" });
            ViewBag.Type = options.Select(x => x.Type).ToList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UserInput([Bind(Include = "start,end,length,HowManyNumbers")] OptionViewModel optionView, string Type)
        {
            List<Option> options = new List<Option>();
            options.Add(new Option() { Type = "Integer" });
            options.Add(new Option() { Type = "Float" });
            options.Add(new Option() { Type = "Hex" });

            if (Type != null)
            {
                optionView.Option = Type;
                return RedirectToAction("Index", optionView);
            }


            ViewBag.Type = options.ToList();

            return View();

        }
        public ActionResult Index(OptionViewModel optionView = null)
        {
            if (optionView.Option == null)
            {
                var listOfIntNumbers = DataPoint.GenerateRandomInt(5000, 1, 100);
                var listOfFloatNumbers = DataPoint.GenerateRandomFloat(5000, 100, 102);
                var listOfHexNumbers = DataPoint.GenerateRandomHex(5000, 4);

                //Above functions returns random values generated by build in Random class. 


                // Count the number of occurances in each list of numbers and add it to the dictionary.
                Dictionary<int, int> OccurenceOfIntNumbers = new Dictionary<int, int>();
                Dictionary<double, int> OccurenceOfFloatNumbers = new Dictionary<double, int>();
                Dictionary<string, int> OccurenceOfHexNumbers = new Dictionary<string, int>();

                //Looping over generated numbers.
                for (int i = 0; i < listOfIntNumbers.Length; i++)
                {
                    int currentNumber = listOfIntNumbers[i];
                    //List of Integer numbers
                    if (OccurenceOfIntNumbers.ContainsKey(currentNumber))
                    {
                        // If dictionary contains the number then increment occurence

                        OccurenceOfIntNumbers[currentNumber] += 1;
                    }
                    else
                    {
                        // If dictionary do not contains the number then add the number and count as 1.
                        OccurenceOfIntNumbers.Add(currentNumber, 1);

                    }

                }

                for (int i = 0; i < listOfFloatNumbers.Length; i++)
                {
                    //List of Float numbers
                    double currentNumber = listOfFloatNumbers[i];
                    //List of Integer numbers
                    if (OccurenceOfFloatNumbers.ContainsKey(currentNumber))
                    {
                        // If dictionary contains the number then increment occurence

                        OccurenceOfFloatNumbers[currentNumber] += 1;
                    }
                    else
                    {
                        // If dictionary do not contains the number then add the number and count as 1.
                        OccurenceOfFloatNumbers.Add(currentNumber, 1);

                    }

                }

                for (int i = 0; i < listOfHexNumbers.Length; i++)
                {
                    //List of Hex numbers
                    //List of Float numbers
                    string currentNumber = listOfHexNumbers[i];
                    //List of Integer numbers
                    if (OccurenceOfHexNumbers.ContainsKey(currentNumber))
                    {
                        // If dictionary contains the number then increment occurence

                        OccurenceOfHexNumbers[currentNumber] += 1;
                    }
                    else
                    {
                        // If dictionary do not contains the number then add the number and count as 1.
                        OccurenceOfHexNumbers.Add(currentNumber, 1);

                    }

                }

                // Generating an instance of datapoints for chart.
                // Here all points can only be doubles. Hex numbers will need to be converted to int.

                List<DataPoint> dataPointsForInt = new List<DataPoint>();

                foreach (var number in OccurenceOfIntNumbers)
                {
                    dataPointsForInt.Add(new DataPoint(number.Key, number.Value));

                }


                ViewBag.DataPoints1 = JsonConvert.SerializeObject(dataPointsForInt);



                List<DataPoint> dataPointsForFloat = new List<DataPoint>();

                foreach (var number in OccurenceOfFloatNumbers)
                {
                    dataPointsForFloat.Add(new DataPoint(number.Key, number.Value));

                }


                ViewBag.DataPoints2 = JsonConvert.SerializeObject(dataPointsForFloat);


                List<DataPoint> dataPointsForHex = new List<DataPoint>();

                foreach (var number in OccurenceOfHexNumbers)
                {
                    dataPointsForHex.Add(new DataPoint(Convert.ToInt32(number.Key, 16), number.Value));
                    // C# supports a type conversion from hex to base 16 int.
                }

                ViewBag.DataPoints3 = JsonConvert.SerializeObject(dataPointsForHex);



                return View();

            }

            else
            {
                if (optionView.Option == "Integer")
                {
                    var listOfIntNumbers = DataPoint.GenerateRandomInt(optionView.HowManyNumbers, optionView.start, optionView.end);

                    //Above functions returns random values generated by build in Random class. 


                    // Count the number of occurances in each list of numbers and add it to the dictionary.
                    Dictionary<int, int> OccurenceOfIntNumbers = new Dictionary<int, int>();

                    //Looping over generated numbers.
                    for (int i = 0; i < listOfIntNumbers.Length; i++)
                    {
                        int currentNumber = listOfIntNumbers[i];
                        //List of Integer numbers
                        if (OccurenceOfIntNumbers.ContainsKey(currentNumber))
                        {
                            // If dictionary contains the number then increment occurence

                            OccurenceOfIntNumbers[currentNumber] += 1;
                        }
                        else
                        {
                            // If dictionary do not contains the number then add the number and count as 1.
                            OccurenceOfIntNumbers.Add(currentNumber, 1);

                        }

                    }


                    // Generating an instance of datapoints for chart.
                    // Here all points can only be doubles. Hex numbers will need to be converted to int.

                    List<DataPoint> dataPointsForInt = new List<DataPoint>();

                    foreach (var number in OccurenceOfIntNumbers)
                    {
                        dataPointsForInt.Add(new DataPoint(number.Key, number.Value));

                    }


                    ViewBag.DataPoints1 = JsonConvert.SerializeObject(dataPointsForInt);
                    ViewBag.DataPoints2 = "";
                    ViewBag.DataPoints3 = "";
           
                }
                else if (optionView.Option == "Float")
                {
                    var listOfFloatNumbers = DataPoint.GenerateRandomFloat(optionView.HowManyNumbers, optionView.start, optionView.end);
                    Dictionary<double, int> OccurenceOfFloatNumbers = new Dictionary<double, int>();

                    for (int i = 0; i < listOfFloatNumbers.Length; i++)
                    {
                        //List of Float numbers
                        double currentNumber = listOfFloatNumbers[i];
                        //List of Integer numbers
                        if (OccurenceOfFloatNumbers.ContainsKey(currentNumber))
                        {
                            // If dictionary contains the number then increment occurence

                            OccurenceOfFloatNumbers[currentNumber] += 1;
                        }
                        else
                        {
                            // If dictionary do not contains the number then add the number and count as 1.
                            OccurenceOfFloatNumbers.Add(currentNumber, 1);

                        }

                    }
                    List<DataPoint> dataPointsForFloat = new List<DataPoint>();

                    foreach (var number in OccurenceOfFloatNumbers)
                    {
                        dataPointsForFloat.Add(new DataPoint(number.Key, number.Value));

                    }


                    ViewBag.DataPoints2 = JsonConvert.SerializeObject(dataPointsForFloat);
                    ViewBag.DataPoints1 = "";
                    ViewBag.DataPoints3 = "";
                }
                else if (optionView.Option == "Hex")
                {
                    var listOfHexNumbers = new string[optionView.HowManyNumbers];
                    if (optionView.length > 0)
                    {
                        listOfHexNumbers = DataPoint.GenerateRandomHex(optionView.HowManyNumbers, 4);
                    }
                    else
                    {
                        listOfHexNumbers = DataPoint.GenerateRandomHex(optionView.HowManyNumbers, optionView.length);

                    }


                    Dictionary<string, int> OccurenceOfHexNumbers = new Dictionary<string, int>();
                    for (int i = 0; i < listOfHexNumbers.Length; i++)
                    {
                        //List of Hex numbers
                        //List of Float numbers
                        string currentNumber = listOfHexNumbers[i];
                        //List of Integer numbers
                        if (OccurenceOfHexNumbers.ContainsKey(currentNumber))
                        {
                            // If dictionary contains the number then increment occurence

                            OccurenceOfHexNumbers[currentNumber] += 1;
                        }
                        else
                        {
                            // If dictionary do not contains the number then add the number and count as 1.
                            OccurenceOfHexNumbers.Add(currentNumber, 1);

                        }

                    }



                    List<DataPoint> dataPointsForHex = new List<DataPoint>();

                    foreach (var number in OccurenceOfHexNumbers)
                    {
                        dataPointsForHex.Add(new DataPoint(Convert.ToInt32(number.Key, 16), number.Value));
                        // C# supports a type conversion from hex to base 16 int.
                    }

                    ViewBag.DataPoints3 = JsonConvert.SerializeObject(dataPointsForHex);
                    ViewBag.DataPoints2 = "";
                    ViewBag.DataPoints1 = "";


                }
                return View();

            }
        }

    }
}