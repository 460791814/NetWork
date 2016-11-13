using UnityEngine;
using System.Collections;

public class BuildNetWork : MonoBehaviour {

    public int connections = 10;//最大连接数
    public int listenPort = 8899;//连接端口；
    public bool useNat = false;//是否使用NAT
    public string local = "127.0.0.1";

    public GameObject cube;
	// Update is called once per frame
	void OnGUI () {

        if (Network.peerType == NetworkPeerType.Disconnected)
        {//如果没有任何连接
            if (GUILayout.Button("创建服务器"))
            {
                //返回连接的错误信息  无错误返回noerror
                NetworkConnectionError error = Network.InitializeServer(connections, listenPort, useNat);
                print(error);
            }
            if (GUILayout.Button("创建客户端"))
            {
                NetworkConnectionError error = Network.Connect(local, listenPort);
                print(error);
            }
        }
        else if (Network.peerType == NetworkPeerType.Server)
        {
            GUILayout.Label("服务器创建完成");
        }
        else if (Network.peerType == NetworkPeerType.Client)
        {
            GUILayout.Label("客户端接入");
        }
        else if (Network.peerType == NetworkPeerType.Connecting)
        {
            GUILayout.Label("Connecting");
        }

	}
    /// <summary>
    /// 当服务被创建的时候会执行 在server端会被调用 client端不会执行
    /// </summary>
    void OnServerInitialized()
    {
        print("服务器创建");
        //Network.player.ToString()  当前端的一个索引 ，，所有的服务端跟客户端都会生成一个唯一的索引 ，这里表示这个创建的物体属于哪个索引对象，我们这是设置的当前
        Network.Instantiate(cube, new Vector3(0, 10, 0), Quaternion.identity, int.Parse(Network.player.ToString()));
    }
    /// <summary>
    /// 当有客户端连接的时候触发 在server端会被调用 client端不会执行
    /// </summary>
    /// <param name="player"></param>
    void OnPlayerConnected(NetworkPlayer player)
    {
        print("有客户端连接  期index:"+player.ToString());
    }
    /// <summary>
    /// 当客户端连接到服务器时触发  在client端会被调用 server端不会执行
    /// </summary>
    void OnConnectedToServer()
    {
        print("连接到服务器");
      Network.Instantiate(cube, new Vector3(0, 10, 0), Quaternion.identity, int.Parse(Network.player.ToString()));
    }

}
