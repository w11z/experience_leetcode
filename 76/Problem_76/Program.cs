var it = new Solution();

var inputs = new List<(string s, string t)>
{
	(s: "BRQPBFAGCRTM", t: "ABC"),
	(s: "a", t: "aa"),
	(s: "aaa", t: "aa"),
	(s: "ababababac", t: "abc"),
};

foreach (var (s, t) in inputs)
{
	Console.WriteLine($"Result ({s}, {t}): '{it.MinWindow(s, t)}'");
}

public class Solution {
	public string MinWindow(string s, string t)
	{
		if (t.Length == 0)
			return "";
		
		var sArray = s.ToCharArray();
		
		string? result = null;

		for (var start = 0; start < sArray.Length; start++)
		{
			var tArray = t.ToArray();
			var tParts = new LinkedList<char>(tArray);
			var tPartsUnique = new HashSet<char>(tArray);

			int? firstEncounterIndex = null;
			int? secondEncounterIndex = null;

			for (var index = start; index < sArray.Length; index++)
			{
				var sChar = sArray[index];
				var foundPendingPart = tParts.Find(sChar);

				if (tPartsUnique.Contains(sChar))
				{
					if (firstEncounterIndex is null)
						firstEncounterIndex = index;
					else if (secondEncounterIndex is null && foundPendingPart is null)
						firstEncounterIndex = index;
					else if (secondEncounterIndex is null && foundPendingPart is not null)
						secondEncounterIndex = index;
				}

				if (foundPendingPart is {} encounter)
				{
					tParts.Remove(encounter);
					if (tParts.Count == 0)
					{
						var firstEncounterIndexValue = firstEncounterIndex!.Value;
						var length = index - firstEncounterIndexValue;
						if (result is null || length < result.Length)
							result = new string(sArray.Skip(firstEncounterIndexValue).Take(length + 1).ToArray());

						start = secondEncounterIndex ?? firstEncounterIndexValue;
						
						break;
					}
				}
			}
		}

		return result ?? "";
	}
}