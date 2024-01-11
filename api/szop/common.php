<?php

include("./db.php");

function userExist($u, $p) {
	global $con;
	
	$result = count($con -> query("SELECT * FROM users WHERE username = '$u' AND password = MD5('$p');") -> fetch_all());
	return $result > 0;
}


?>