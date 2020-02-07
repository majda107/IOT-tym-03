console.log('creating socket...')

var socket = new WebSocket("ws://192.168.43.169:5050", "protocolOne")
socket.onopen = () => {
    console.log("connected! sending data...")
    socket.send("lmao test");
    console.log("data sent!")
};