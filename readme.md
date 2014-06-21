Java and PHP classes used to encrypt and decrypt strings with a private key, or any key scheme that you create, and pass data between PHP and Java clients or servers.





***

Java Dependencies  
- [Apache Commons Coded](http://commons.apache.org/codec/)

***


iOS Usage  

- In the ***arc*** project, you have to add the `-fno-objc-arc` in the build phases.
- Encrypt and Decrypt in this way:
```
NSString *encStr = [AESTool encryptData:@"hello" withKey:@"1234567890123456"];
NSLog(@"%@", encStr);
```

***

Tips

- While you send a ***POST*** or ***GET*** request with data using the AES encrypt, the `+` will replaced by ' '.To solve this this problem, you can do like this to replace the `+` with `-`:

```Java
public static String encrypt(String input, String key) {
    byte[] crypted = null;
    try {
        SecretKeySpec skey = new SecretKeySpec(key.getBytes(), "AES");
        Cipher cipher = Cipher.getInstance("AES/ECB/PKCS5Padding");
        cipher.init(Cipher.ENCRYPT_MODE, skey);
        crypted = cipher.doFinal(input.getBytes());
    } catch (Exception e) {
        System.out.println(e.toString());
    }
    String result = new String(Base64.encodeBase64(crypted));
    return result.replace("+", "-");

}

public static String decrypt(String input, String key) {
    input = input.replace("-", "+");
    byte[] output = null;
    try {
        SecretKeySpec skey = new SecretKeySpec(key.getBytes(), "AES");
        Cipher cipher = Cipher.getInstance("AES/ECB/PKCS5Padding");
        cipher.init(Cipher.DECRYPT_MODE, skey);
        output = cipher.doFinal(Base64.decodeBase64(input));
    } catch (Exception e) {
        System.out.println("after encrypt : "+ e.toString());
    }
    return new String(output);
}
```


```PHP
public function encrypt($input, $key) 
{
    $size = mcrypt_get_block_size(MCRYPT_RIJNDAEL_128, MCRYPT_MODE_ECB); 
    $input = $this->pkcs5_pad($input, $size); 
    $td = mcrypt_module_open(MCRYPT_RIJNDAEL_128, '', MCRYPT_MODE_ECB, ''); 
    $iv = mcrypt_create_iv (mcrypt_enc_get_iv_size($td), MCRYPT_RAND); 
    mcrypt_generic_init($td, $key, $iv); 
    $data = mcrypt_generic($td, $input); 
    mcrypt_generic_deinit($td); 
    mcrypt_module_close($td); 
    $data = base64_encode($data);
    $data = str_replace("+","-",$data);
    return $data; 
} 
public function decrypt($sStr, $sKey) {
    $sStr = str_replace("-","+",$sStr);
    $decrypted= mcrypt_decrypt(
        MCRYPT_RIJNDAEL_128,
        $sKey, 
        base64_decode($sStr), 
        MCRYPT_MODE_ECB
    );
    $dec_s = strlen($decrypted); 
    $padding = ord($decrypted[$dec_s-1]); 
    $decrypted = substr($decrypted, 0, -$padding);
    return $decrypted;
}
```

***


Steven Holder
http://stevenholder.com


Copyright (C) 2011 by Steven Holder

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.