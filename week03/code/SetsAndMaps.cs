using System.Text.Json;
using cse212;

public static class SetsAndMaps
{
    /// <summary>
    /// The words parameter contains a list of two character 
    /// words (lower case, no duplicates). Using sets, find an O(n) 
    /// solution for returning all symmetric pairs of words.  
    ///
    /// For example, if words was: [am, at, ma, if, fi], we would return :
    ///
    /// ["am & ma", "if & fi"]
    ///
    /// The order of the array does not matter, nor does the order of the specific words in each string in the array.
    /// at would not be returned because ta is not in the list of words.
    ///
    /// As a special case, if the letters are the same (example: 'aa') then
    /// it would not match anything else (remember the assumption above
    /// that there were no duplicates) and therefore should not be returned.
    /// </summary>
    /// <param name="words">An array of 2-character words (lowercase, no duplicates)</param>
    public static string[] FindPairs(string[] words)
    {
        // Step 01: We search using HashSet O(1)
        HashSet<string> seenWords = new HashSet<string>();

        // Step 01.1: Create a temporary list to store the matching pairs
        List<string> results = new List<string>();

        foreach (var word in words)
        {
            // Step 02: Create a copy of the inverted word
            string reversed = $"{word[1]}{word[0]}";

            // Step 03: Compare if the reversed version exist.
            if (seenWords.Contains(reversed))
            {
                results.Add($"{reversed} & {word}");
            }
            else
            {
                // Step 04: If doesn't match, store for future comparison.
                seenWords.Add(word);
            }
        }

        return results.ToArray();
    }

    /// <summary>
    /// Read a census file and summarize the degrees (education)
    /// earned by those contained in the file.  The summary
    /// should be stored in a dictionary where the key is the
    /// degree earned and the value is the number of people that 
    /// have earned that degree.  The degree information is in
    /// the 4th column of the file.  There is no header row in the
    /// file.
    /// </summary>
    /// <param name="filename">The name of the file to read</param>
    /// <returns>fixed array of divisors</returns>
    public static Dictionary<string, int> SummarizeDegrees(string filename)
    {
        var degrees = new Dictionary<string, int>();
        foreach (var line in File.ReadLines(filename))
        {
            var fields = line.Split(",");

            // Step 01: Check whether the row has enough columns to avoid index errors
            if (fields.Length >= 4)
            {
                // Step 02: The grade is in column 4 (index 3)
                string degree = fields[3].Trim();

                if (degrees.ContainsKey(degree))
                {
                    // Step 02.1: If it already exists, we increase the likelihood of an exact match by adding 1
                    degrees[degree]++;
                }
                else
                {
                    // Step 02.2: If this is the first time we're seeing it, we initialize it
                    degrees[degree] = 1;
                }
            }
        }

        return degrees;
    }

    /// <summary>
    /// Determine if 'word1' and 'word2' are anagrams.  An anagram
    /// is when the same letters in a word are re-organized into a 
    /// new word.  A dictionary is used to solve the problem.
    /// 
    /// Examples:
    /// is_anagram("CAT","ACT") would return true
    /// is_anagram("DOG","GOOD") would return false because GOOD has 2 O's
    /// 
    /// Important Note: When determining if two words are anagrams, you
    /// should ignore any spaces.  You should also ignore cases.  For 
    /// example, 'Ab' and 'Ba' should be considered anagrams
    /// 
    /// Reminder: You can access a letter by index in a string by 
    /// using the [] notation.
    /// </summary>
    public static bool IsAnagram(string word1, string word2)
    {
        // Step 01: formatting for each word
        string w1 = word1.ToLower().Replace(" ", "");
        string w2 = word2.ToLower().Replace(" ", "");

        // Step 02: Stop the execution if the size does not match.
        if (w1.Length != w2.Length) return false;

        // Step 03: The dictionary is defined
        var counts = new Dictionary<char, int>();

        // Step 04: Store frecuency
        foreach (char c in w1)
        {
            if (counts.ContainsKey(c)) counts[c]++;
            else counts[c] = 1;
        }

        // Step 05: We check against the second word
        foreach (char c in w2)
        {
            if (!counts.ContainsKey(c) || counts[c] == 0) return false;
            counts[c]--;
        }

        return true;
    }
    /// <summary>
    /// This function will read JSON (Javascript Object Notation) data from the 
    /// United States Geological Service (USGS) consisting of earthquake data.
    /// The data will include all earthquakes in the current day.
    /// 
    /// JSON data is organized into a dictionary. After reading the data using
    /// the built-in HTTP client library, this function will return a list of all
    /// earthquake locations ('place' attribute) and magnitudes ('mag' attribute).
    /// Additional information about the format of the JSON data can be found 
    /// at this website:  
    /// 
    /// https://earthquake.usgs.gov/earthquakes/feed/v1.0/geojson.php
    /// 
    /// </summary>
    public static string[] EarthquakeDailySummary()
    {
        // Step 01: Define the endpoint URI for the USGS earthquake data
        const string uri = "https://earthquake.usgs.gov/earthquakes/feed/v1.0/summary/all_day.geojson";

        // Step 02: Setup the HTTP client and request message
        using var client = new HttpClient();
        using var getRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

        // Step 03: Execute the request and read the JSON response as a string
        using var response = client.Send(getRequestMessage);
        using var jsonStream = response.Content.ReadAsStream();
        using var reader = new StreamReader(jsonStream);
        var json = reader.ReadToEnd();

        // Step 04: Configure JSON deserialization options to be case-insensitive
        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

        // Step 05: Deserialize the JSON string into FeatureCollection class structure
        var featureCollection = JsonSerializer.Deserialize<FeatureCollection>(json, options);

        // Step 06: Validate the data and handle null responses to prevent runtime errors
        if (featureCollection?.Features == null)
        {
            return Array.Empty<string>();
        }

        // Step 07: Iterate through the features to extract place and magnitude
        var summary = new List<string>();
        foreach (var feature in featureCollection.Features)
        {
            string place = feature.Properties.Place;
            double mag = feature.Properties.Mag;

            // Step 08: Format the earthquake data into a descriptive string
            summary.Add($"{place} - Mag {mag}");
        }

        return summary.ToArray();
    }
}