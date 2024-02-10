from flask import Flask, render_template, request, jsonify

app = Flask(__name__)

@app.route('/')
def index():
    return render_template('index.html')

@app.route('/ping', methods=['POST'])
def ping():
    print("Pong!")
    return jsonify({"message": "Pong!"})

if __name__ == '__main__':
    app.run(debug=True)
