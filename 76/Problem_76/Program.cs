var it = new Solution();

var inputs = new List<(string s, string t)>
{
	(s: "BRQPBFAGCRTM", t: "ABC"),
	(s: "a", t: "aa"),
};

foreach (var (s, t) in inputs)
{
	Console.WriteLine($"Result ({s}, {t}): '{it.MinWindow(s, t)}'");
}

public class Solution {
	public string MinWindow(string s, string t)
	{
		var sArray = s.ToCharArray();
		
		string? result = null;

		for (var start = 0; start < sArray.Length; start++)
		{
			var tParts = new LinkedList<char>(t.ToArray());

			for (var index = start; index < sArray.Length; index++)
			{
				var sChar = sArray[index];
				if (tParts.Find(sChar) is {} encounter)
				{
					tParts.Remove(encounter);
					if (tParts.Count == 0)
					{
						var length = index - start;
						if (result is null || length < result.Length)
							result = new string(sArray.Skip(start).Take(length + 1).ToArray());
						
						break;
					}
				}
			}
		}

		return result ?? "";
	}
}