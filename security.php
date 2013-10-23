<?php
class Security{
	public static function encrypt($input, $key) {
		$input = self::pkcs5_pad($input, mcrypt_get_block_size(MCRYPT_RIJNDAEL_128, MCRYPT_MODE_ECB));
		$td    = mcrypt_module_open(MCRYPT_RIJNDAEL_128, '', MCRYPT_MODE_ECB, '');
		mcrypt_generic_init($td,$key,mcrypt_create_iv(mcrypt_enc_get_iv_size($td), MCRYPT_RAND));
		$input = mcrypt_generic($td,$input);
		mcrypt_generic_deinit($td);
		mcrypt_module_close($td);
		return base64_encode($input);
	}

	private static function pkcs5_pad($text, $blocksize){
		$pad = $blocksize - (strlen($text) % $blocksize);
		return $text . str_repeat(chr($pad), $pad);
	}

	public static function decrypt($sStr, $sKey){
		$decrypted = mcrypt_decrypt(MCRYPT_RIJNDAEL_128,$sKey,base64_decode($sStr),MCRYPT_MODE_ECB);
		return substr($decrypted, 0, -ord($decrypted[strlen($decrypted)-1]));
	}
}
?>
