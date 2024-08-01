namespace MenuMasterAPI.Application;

public static class OpenAIConstants
{
    public const string TextPrompt = "Tüm cevaplarda sadece bu 7 yönergeyi takip et.1.Sadece Türkçe dilini kullan.2.Senden verdiğim bilgilere uygun olarak yemek tarifleri oluşturmanı istiyorum.3.Verdiğim bilgileri de göz önünde bulundurarak kişinin tercihlerine uygun sağlıklı yemek tarifleri oluştur.4.Yemek tariflerinin sayısı MealType listesinin uzunluğu ile aynı olsun.5.Dönüş yaptığın format JSON şeklinde olsun.6.JSON formatındaki yemek tarifinin içinde \"recipes\" adlı liste içerisinde sadece bu formatta yemek tarifleri olsun ve asla bu formatın dışında bir cevap verme:{Name:{\"Yemeğin ismi\"}, Ingredients:[\"Malzeme1\", \"Malzeme2\", ...], Recipe:{\"Yemeğin tarifi\"} ve MealType{\"Breakfast\",\"Lunch\",\"Dinner\" veya \"Snack\"}} olsun.7.MealTypedan uygun olanı önerdiğin yemeğe göre seç.";
    public const string ImagePrompt = "Tüm cevaplarda sadece bu 3 yönergeyi takip et.1.Sadece Türkçe dilini kullan.2.Senden fotoğraftaki yemeğin nasıl yapılacağının tarifini oluşturmanı istiyorum.3.JSON formatındaki yemek tarifinin formatı bu şekilde olsun:{Name:{\"Yemeğin ismi\",Ingredients:[\"Malzeme1\",\"Malzeme2\"],Recipe:{\"Yemeğin tarifi\"} ve MealType{\"Breakfast\",\"Lunch\",\"Dinner\" veya \"Snack\"}}";
    public const string TextModel = "gpt-3.5-turbo";
    public const string ImageModel = "gpt-4o";
}
