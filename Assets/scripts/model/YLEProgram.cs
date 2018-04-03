using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Meta
{
    public string offset;
    public string limit;
    public int count;
    public int program;
    public int clip;
    public string q;
}

[System.Serializable]
public class Description
{
    public string fi;
    public string sv;
}

[System.Serializable]
public class Format
{
    public string inScheme;
    public string type;
    public string key;
}

[System.Serializable]
public class Video
{
    public IList<object> language;
    public IList<Format> format;
    public string type;
}

[System.Serializable]
public class Creator
{
    public string name;
    public string type;
}

[System.Serializable]
public class Title
{
	public string fi;
    public string sv;
	public string en;
}

[System.Serializable]
public class CoverImage
{
    public string id;
    public bool available;
    public string type;
    public int version;
}

[System.Serializable]
public class Image
{
    public string id;
    public bool available;
    public string type;
    public int version;
}

[System.Serializable]
public class Broader
{
    public string id;
}

[System.Serializable]
public class Subject
{
	public string id;
	public Title title;
	public string inScheme;
	public string type;
	public Broader broader;
	public string key;
	public IList<Notation> notation;
}

[System.Serializable]
public class Interaction
{
    public Title title;
    public string type;
    public string url;
}

[System.Serializable]
public class AvailabilityDescription
{
    public string sv;
}

[System.Serializable]
public class PartOfSeries
{
    public Description description;
    public IList<object> creator;
    public string type;
    public Title title;
    public CoverImage coverImage;
    public IList<object> countryOfOrigin;
    public string id;
    public Image image;
    public IList<Subject> subject;
    public IList<Interaction> interactions;
    public IList<string> alternativeId;
    public AvailabilityDescription availabilityDescription;
}

[System.Serializable]
public class ContentRating
{
    public Title title;
    public IList<object> reason;
}

[System.Serializable]
public class ItemTitle
{
    public string fi;
    public string sv;
	public string en;
}
	
[System.Serializable]
public class Audio
{
    public IList<string> language;
    public IList<Format> format;
    public string type;
}

[System.Serializable]
public class OriginalTitle
{
}

[System.Serializable]
public class Tags
{
    public bool catalog;
}

[System.Serializable]
public class Service
{
    public string id;
}

[System.Serializable]
public class ContentProtection
{
    public string id;
    public string type;
}

[System.Serializable]
public class Media
{
    public string id;
    public string duration;
    public IList<ContentProtection> contentProtection;
    public bool available;
    public string type;
    public bool downloadable;
    public int version;
}

[System.Serializable]
public class Publisher
{
    public string id;
}

[System.Serializable]
public class PublicationEvent
{
    public Tags tags;
    public Service service;
    public System.DateTime startTime;
    public string temporalStatus;
    public System.DateTime endTime;
    public string type;
    public string duration;
    public string region;
    public string id;
    public int version;
    public Media media;
    public IList<Publisher> publisher;
}

[System.Serializable]
public class Notation
{
    public string value;
    public string valueType;
}

[System.Serializable]
public class Datum
{
    public Description description;
    public Video video;
    public string typeMedia;
    public IList<Creator> creator;
    public PartOfSeries partOfSeries;
    public System.DateTime indexDataModified;
    public string type;
    public string duration;
    public ContentRating contentRating;
    public Title title;
    public ItemTitle itemTitle;
    public IList<string> countryOfOrigin;
    public string id;
    public string typeCreative;
    public Image image;
    public IList<Audio> audio;
    public OriginalTitle originalTitle;
    public IList<PublicationEvent> publicationEvent;
    public string collection;
    public IList<Subject> subject;
    public IList<object> subtitling;
    public string productionId;
    public IList<string> alternativeId;
}

[System.Serializable]
public class YLEProgram
{
    public string apiVersion;
    public Meta meta;
	public Datum[] data;
}

