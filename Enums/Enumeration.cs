using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace WorkCloudTest.Enums
{
    public abstract class Enumeration : IComparable
	{
		public string Name { get; private set; }

		public int Value { get; private set; }

		protected Enumeration()
		{
		}

		protected Enumeration(int value, string name)
		{
			Value = value;
			Name = name;
		}

		public override string ToString() => Name;

		public static IEnumerable<T> GetAll<T>() where T : Enumeration
		{
			var fields = typeof(T).GetFields(BindingFlags.Public |
											BindingFlags.Static |
											BindingFlags.DeclaredOnly);

			return fields.Select(f => f.GetValue(null)).Cast<T>();
		}

		public override bool Equals(object obj)
		{
			var otherValue = obj as Enumeration;

			if (otherValue == null)
				return false;

			var typeMatches = GetType().Equals(obj.GetType());
			var valueMatches = Value.Equals(otherValue.Value);

			return typeMatches && valueMatches;
		}

		public static T FromValue<T>(int value) where T : Enumeration, new()
		{
			var matchingItem = parse<T, int>(value, "value", item => item.Value == value);
			return matchingItem;
		}

		public static T FromName<T>(string name) where T : Enumeration, new()
		{
			var matchingItem = parse<T, string>(name, "name", item => item.Name.ToUpper() == name.ToUpper());
			return matchingItem;
		}

		public static bool ExistName<T>(string name) where T : Enumeration, new()
		{
			try
			{
				T matchingItem = parse<T, string>(name, "name", item => item.Name.ToUpper() == name.ToUpper());

				return matchingItem != null;
			}
			catch (ApplicationException)
			{
				return false;
			}
			catch (Exception e)
			{
				throw new Exception("Error to more details see inner Exception", e);
			}
		}

		public static bool ExistValue<T>(int value) where T : Enumeration, new()
		{
			try
			{
				T matchingItem = parse<T, int>(value, "value", item => item.Value == value);

				return matchingItem != null;
			}
			catch (ApplicationException)
			{
				return false;
			}
			catch (Exception e)
			{
				throw new Exception("Error to more details see inner Exception", e);
			}
		}

		private static T parse<T, K>(K value, string description, Func<T, bool> predicate) where T : Enumeration, new()
		{
			var matchingItem = GetAll<T>().FirstOrDefault(predicate);

			if (matchingItem == null)
			{
				var message = string.Format("'{0}' is not a valid {1} in {2}", value, description, typeof(T));
				throw new ApplicationException(message);
			}

			return matchingItem;
		}

		public int CompareTo(object other) => Value.CompareTo(((Enumeration)other).Value);

		public override int GetHashCode()
		{
			return HashCode.Combine(Name, Value);
		}
	}
}
