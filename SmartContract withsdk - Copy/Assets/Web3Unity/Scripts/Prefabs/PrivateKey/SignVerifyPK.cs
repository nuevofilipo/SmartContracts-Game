using System.Collections;
using System.Collections.Generic;
using Nethereum.Hex.HexConvertors.Extensions;
using UnityEngine;

public class SignVerifyPK : MonoBehaviour
{
    // Start is called before the first frame update
    async public void TestSigning()
    {
        var privateKey = "0b3e58026e8330d12a659a3e89aa7c5622d716ac67c42d93a7599276f9c26fa3";
        string message = "hello";
        var hashedMessage = Web3Wallet.Sha3(message);
        Debug.Log("Hashed Message PK: " + hashedMessage);
        string signature = Web3PrivateKey.Sign(privateKey, message);
        print("Signature PK: " + signature);
        // get account from private key
        string account = Web3PrivateKey.Address(privateKey);
        print("Account from PK: " + account);
        string address = await EVM.Verify(hashedMessage, signature);
        print("Address From Verify PK: " + address);

        ParseSignatureFunction(signature);
    }
    // Update is called once per frame
    public void ParseSignatureFunction(string sig)
    {
        string signature = sig;
        string r = signature.Substring(0, 66).EnsureHexPrefix();
        Debug.Log("PK R:" + r);
        string s = signature.Substring(66, 64).EnsureHexPrefix();
        Debug.Log("PK S: " + s);
        int v = int.Parse(signature.Substring(130, 2), System.Globalization.NumberStyles.HexNumber);
        Debug.Log("PK V: " + v);
    }
}