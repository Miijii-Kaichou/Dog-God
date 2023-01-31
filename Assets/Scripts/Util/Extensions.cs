using SharedData;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Extensions
{
    public static class Extensions
    {
        /// <summary>
        /// Nullify an object
        /// </summary>
        /// <param name="obj"></param>
        public static void Nullify(this object obj) => obj = null;

        /// <summary>
        /// Will return an object type only if it's null
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static T OnlyIfNull<T>(this T obj)
        {
            if (obj != null) return (T)obj;
            return default;
        }

        /// <summary>
        /// Will convert a value to a sign value
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Sign ToSign(this int value)
        {
            return value < 0 ? Sign.Negative : value > 0 ? Sign.Positive : Sign.Zero;
        }

        public static Sign ToSign(this float value)
        {
            return value < 0f ? Sign.Negative : value > 0f ? Sign.Positive : Sign.Zero;
        }

        /// <summary>
        /// Will convert a value to it's absolute equivalent
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static int ToAbsolute(this int value)
        {
            return value < 0 ? value * -1 : value;
        }

        /// <summary>
        /// Will return the digit's length
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static int DigitLength(this int value)
        {

            return value.ToString().Length;
        }

        /// <summary>
        /// Will return if a number is in the 1s, 10s, 100s,
        /// or 1000s
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static int DigitPlace(this int value)
        {
            if (value < 0) return 0;

            int placement = 1;
            const int TEN = 10;
            for (int i = 0; i < value.DigitLength(); i++)
            {
                placement *= i == 0 ? placement : TEN;
            }

            return placement;
        }


        /// <summary>
        /// Will convert a number to zero-base
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static int ZeroBased(this int value)
        {
            return value - 1;
        }

        /// <summary>
        /// Will convert a number to zero-base
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static float ZeroBased(this float value)
        {
            return value - 1f;
        }

        /// <summary>
        /// Will convert a number to zero-base
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static double ZeroBased(this double value)
        {
            return value - 1d;
        }

        /// <summary>
        /// Will convert a number to one-base
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static int OneBased(this int value)
        {
            return value + 1;
        }

        /// <summary>
        /// Will convert a number to one-base
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static float OneBased(this float value)
        {
            return value + 1f;
        }

        /// <summary>
        /// Will convert a number to one-base
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static double OneBased(this double value)
        {
            return value + 1d;
        }

        /// <summary>
        /// Will convert a number to it's boolean equivalent
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool ToBool(this int value)
        {
            return value == 1 ? true : false;
        }

        /// <summary>
        /// Will convert any object to it's boolean equivalent
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool ToBool(this object obj)
        {
            return (bool)obj;
        }


        public static bool Give(this bool obj, ref bool sharedObject)
        {
            sharedObject = obj;
            return sharedObject;
        }

        /// <summary>
        /// Will produce a sum based on the initial value
        /// </summary>
        /// <param name="_"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        public static int Sum(this ref int _, params int[] values)
        {
            if (values == null || values.Length == 0)
                return 0;

            for (int i = 0; i < values.Length; i++)
            {
                _ += values[i];
            }
            return _;
        }

        /// <summary>
        /// Check if an object is of a certain type
        /// </summary>
        /// <param name="_"></param>
        /// <param name="character"></param>
        /// <returns></returns>
        public static bool Is(this object _, Type type)
        {
            return _.GetType().Equals(type);
        }


        /// <summary>
        /// Cast a target to a different type
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="_"></param>
        /// <returns></returns>
        public static T CastTo<T>(this object _)
        {
            try
            {
                T data = (T)_;
                return data;
            }
            catch
            {
                Debug.LogError($"Invalid casting of object {_}");
                return default;
            }
        }

        /// <summary>
        /// Will grab multiple component from one object. It's best to only call this
        /// one to cache those objects.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="_"></param>
        /// <returns></returns>
        public static T[] GrabComponents<T>(this UnityEngine.Component[] _)
        {
            try
            {
                T[] data = new T[_.Length];
                for (int i = 0; i < _.Length; i++)
                {
                    data[i] = _[i].GetComponent<T>();
                }
                return data;
            }
            catch
            {
                return default;
            }
        }

        /// <summary>
        /// Will grab multiple component from one object. It's best to only call this
        /// one to cache those objects.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="_"></param>
        /// <returns></returns>
        public static T[] GrabComponents<T>(this UnityEngine.GameObject[] _)
        {
            try
            {
                T[] data = new T[_.Length];
                for (int i = 0; i < _.Length; i++)
                {
                    data[i] = _[i].GetComponent<T>();
                }
                return data;
            }
            catch
            {
                return default;
            }
        }

        /// <summary>
        /// Increment by 1
        /// </summary>
        /// <param name="_"></param>
        /// <returns></returns>
        public static int Next(this ref int _)
        {
            _++;
            return _;
        }

        /// <summary>
        /// Set a number as negative
        /// </summary>
        /// <param name="_"></param>
        /// <returns></returns>
        public static int Negate(this int _)
        {
            _ *= -1;
            return _;
        }

        /// <summary>
        /// Set a number as negative
        /// </summary>
        /// <param name="_"></param>
        /// <returns></returns>
        public static float Negate(this float _)
        {
            _ *= -1;
            return _;
        }

        public static int Ceil(this float _)
        {
            return Mathf.CeilToInt(_);
        }

        public static bool AtLeast1True(this bool[] _)
        {
            bool conditionMet = false;
            for (int i = 0; i < _.Length; i++)
            {
                if (_[i] == true)
                    conditionMet = _[i];
            }
            return conditionMet;
        }

        public static T Get<T>(this T[] _, int index)
        {
            return _[index];
        }
        public static T Get<T>(this ICollection<T> _, int index)
        {
            return _.ElementAt(index);
        }

        public static void Enable(this GameObject _) => _.SetActive(true);
        public static void Enable(this Behaviour _) => _.enabled = true;
        public static void Disable(this GameObject _) => _.SetActive(false);
        public static void Disable(this Behaviour _) => _.enabled = false;
    }

    /// <summary>
    /// Global Coroutine Extension
    /// </summary>
    /// <remarks>For best practices, only used this extension for classes not deriving from monobehviours, or cannot call "StartCoroutine"</remarks>
    public static class Coroutine
    {
        public static void Start(this IEnumerator enumerator)
        {
            if (enumerator == null) return;

            CoroutineHandler.Execute(enumerator);
        }

        public static void Stop(this IEnumerator enumerator)
        {
            if (enumerator == null) return;

            CoroutineHandler.Halt(enumerator);
        }

        public static void StopAll()
        {
            CoroutineHandler.ClearRoutines();
        }
    }

    /// <summary>
    /// A custom variable class
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [ImmutableObject(true), Serializable]
    public struct Var<T>
    {
        public T Value;

        public Var(T value)
        {
            Value = value;
        }

        public static Var<T> operator +(Var<T> a, Var<T> b)
        {
            return a + b;
        }

        public static Var<T> operator -(Var<T> a, Var<T> b)
        {
            return a - b;
        }

        public static implicit operator Var<T>(T value)
        {
            return new Var<T>(value);
        }
    }

    /// <summary>
    /// Extension Class for Dictionaries
    /// </summary>
    public static class Dictionary
    {
        public static K GetKey<K, V>(this Dictionary<K, V> keyValuePairs, V value)
        {
            foreach (KeyValuePair<K, V> keyValuePair in keyValuePairs)
            {
                if (value.ToString() == keyValuePair.Value.ToString())
                    return keyValuePair.Key;
            }
            return default;
        }

        public static V GetValue<K, V>(this Dictionary<K, V> keyValuePairs, K key)
        {
            foreach (KeyValuePair<K, V> keyValuePair in keyValuePairs)
            {
                if (key.ToString() == keyValuePair.Key.ToString())
                    return keyValuePair.Value;
            }
            return default;
        }
    }

    /// <summary>
    /// Extension Class for Booleans
    /// </summary>
    public static class Boolean
    {
        public static int AsNumericValue(this bool boolean) => boolean ? 1 : 0;
        public static void Set(this bool _, bool value) { _ = value; }
        public static bool Not(this bool _) => !_;
    }

    /// <summary>
    /// Extension Class for Arrays
    /// </summary>
    public static class Array
    {
        public static string[] ToStringArray(this int[] _)
        {
            return ToStringArray(_);
        }

        public static string[] ToStringArray(this float[] _)
        {
            return ToStringArray(_);
        }

        public static string[] ToStringArray(this double[] _)
        {
            return ToStringArray(_);
        }

        public static string[] ToStringArray(this object[] _)
        {
            string[] stringArray = new string[_.Length];
            for (int i = 0; i < _.Length; i++)
            {
                stringArray[i] = _[i].ToString();
            }
            return stringArray;
        }

        public static string[] ToStringArray(this object[] _, char delimiter, int index)
        {
            string[] stringArray = new string[_.Length];
            for (int i = 0; i < _.Length; i++)
            {
                stringArray[i] = _[i].ToString().Split(delimiter)[index];
            }
            return stringArray;
        }

        public static int Sum(this int[] _)
        {
            int value = 0;
            for (int i = 0; i < _.Length; i++)
            {
                value += _[i];
            }

            return value;
        }

        #region Unity Specific
        public static string[] ToStringArray(this UnityEngine.Object[] _)
        {
            string[] stringArray = new string[_.Length];
            for (int i = 0; i < _.Length; i++)
            {
                stringArray[i] = _[i].ToString();
            }
            return stringArray;
        }

        public static string[] ToStringArray(this Resolution[] _, string removeString)
        {
            string[] stringArray = new string[_.Length];
            for (int i = 0; i < _.Length; i++)
            {
                stringArray[i] = _[i].ToString().Replace(removeString, string.Empty);
            }
            return stringArray;

        }
        #endregion
    }

    /// <summary>
    /// Extension Class for Strings
    /// </summary>
    public static class String
    {
        public static string TryConcat(this string _, params string[] strings)
        {
            StringBuilder stringBuilder = new StringBuilder();
            try
            {
                foreach (string _string in strings)
                {
                    stringBuilder = stringBuilder.AppendLine(_string);
                }
                return stringBuilder.ToString();
            }
            catch (IOException e)
            {
                Debug.Log(string.Format("Failed to concatenate: {0}", e.Message));
                return string.Empty;
            }
        }

        public static int AsNumericalValue(this string _)
        {
            return Convert.ToInt32(_);
        }

        public static string QuestionMark(this string _)
        {
            _ = "???";
            return _;
        }

        /// <summary>
        /// Generate a number based on a string.
        /// Also known as the String Numeric Value (SNV)
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static int GenerateSNV(this string str)
        {
            //Get length of string
            int length = str.Length;

            //Collect ASCII code from string
            int[] asciiList = new int[length];


            //Iterate trhough string, and assign acsii values
            //Start with the length, and increment each iteration
            for (int limit = 0; limit < length; limit++)
            {
                for (int i = 0; i < length - limit; i++)
                {
                    var increment = asciiList[i] + i + 1 + str[0] + str[length - 1];
                    asciiList[i] = increment + str[i] + length - limit + 1;
                }
            }

            //Add all asciiList values
            return asciiList.Sum();
        }
    }

    public static class File
    {
        public static void TryCopy(string sourceName, string destination)
        {
            try
            {
                System.IO.File.Copy(sourceName, destination);
            }
            catch (IOException e)
            {
                Debug.Log(string.Format("Failed to Copy File Info: {0}", e.Message));
                return;
            }
        }
    }
}
