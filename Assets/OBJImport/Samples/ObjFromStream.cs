using Dummiesman;
using System.IO;
using System.Text;
using UnityEngine;

public class ObjFromStream : MonoBehaviour {
	void Start () {
        //make www
        var www = new WWW("localhost:3000/phone.obj");
        var www2 = new WWW("localhost:3000/phone.mtl");
        while (!www.isDone)
            System.Threading.Thread.Sleep(1);
        
        //create stream and load
        var textStream = new MemoryStream(Encoding.UTF8.GetBytes(www.text));
        var mtlStream = new MemoryStream(Encoding.UTF8.GetBytes(www2.text));
        var loadedObj = new OBJLoader().Load(textStream, mtlStream);
	}
}
