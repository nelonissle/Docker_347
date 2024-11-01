<?php
header("Access-Control-Allow-Origin: *"); // Allow cross-origin requests for development
header("Content-Type: application/json");

// Database connection details
$servername = "localhost"; // or the IP address of your MySQL server
$username = "root"; // replace with your HeidiSQL/MySQL username
$password = "290302"; // replace with your HeidiSQL/MySQL password
$dbname = "ticketsystem";

// Connect to MySQL
$conn = new mysqli($servername, $username, $password, $dbname);

// Check connection
if ($conn->connect_error) {
    die(json_encode(["status" => "error", "message" => "Connection failed: " . $conn->connect_error]));
}

// Retrieve and decode JSON data
$data = json_decode(file_get_contents("php://input"), true);
$email = $data['email'];
$name = $data['name'];
$message = $data['message'];
$topic = $data['topic'];
$captcha = $data['captcha'];

// Validate CAPTCHA (stored CAPTCHA should be passed in the same request for simplicity)
session_start();
if ($captcha !== $_SESSION['generatedCaptcha']) {
    echo json_encode(["status" => "error", "message" => "Captcha verification failed!"]);
    exit;
}

// Insert data into MySQL
$query = "INSERT INTO tickets (email, name, message, topic, captcha) VALUES (?, ?, ?, ?, ?)";
$stmt = $conn->prepare($query);
$stmt->bind_param("sssss", $email, $name, $message, $topic, $captcha);

if ($stmt->execute()) {
    echo json_encode(["status" => "success", "message" => "Ticket successfully submitted"]);
} else {
    echo json_encode(["status" => "error", "message" => "Error saving the ticket: " . $stmt->error]);
}

// Close connection
$stmt->close();
$conn->close();
?>
