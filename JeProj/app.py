from flask import Flask, request, render_template, jsonify

app = Flask(__name__)

@app.route('/receive', methods=['POST'])
def receive_pong():
    data = request.data  # Access the plain string data directly
    if data:
        print(f"Received message: {data.decode('utf-8')}")  # Decode byte string to UTF-8
        return {"status": "success", "message": "Pong received!"}, 200
    else:
        return {"status": "error", "message": "No message received"}, 400

color_change_requested = False

@app.route('/')
def index():
    return render_template('button.html')

@app.route('/change-color', methods=['POST'])
def change_color():
    global color_change_requested
    color_change_requested = True
    return jsonify({"message": "Color change request received"}), 200

@app.route('/check-color-change', methods=['GET'])
def check_color_change():
    global color_change_requested
    if color_change_requested:
        color_change_requested = False
        return jsonify({"changeColor": True}), 200
    else:
        return jsonify({"changeColor": False}), 200

if __name__ == '__main__':
    app.run(debug=True, host='0.0.0.0', port=5000)
