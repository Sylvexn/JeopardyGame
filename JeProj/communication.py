from flask import Flask, request, render_template, jsonify
import discord
import asyncio

app = Flask(__name__)

intents = discord.Intents.default()
intents.messages = True
intents.members = True
client = discord.Client(intents=intents)
clientID = "995411416590848071"
token = "OTk1NDExNDE2NTkwODQ4MDcx.GG4nAd.F3-6RfNBUTz0iExCBcYYE-mWQzf1UFdSnjlhqY"
sylUID = "138887188468203520"

@client.event
async def on_ready():
    print(f'Logged in as {client.user}')

@app.route('/')
def index():
    return render_template('index.html')

@app.route('/mute', methods=['POST'])
def mute_user():
    user_id = request.form['userId']
    reason = "Muted via Unity interaction"
    asyncio.run_coroutine_threadsafe(mute_user_coroutine(user_id, reason), client.loop)
    return 'User mute requested'

async def mute_user_coroutine(user_id, reason, should_mute=True):
    guild_id = 861725260516687892  # Replace with your actual guild ID
    guild = client.get_guild(guild_id)
    
    if not guild:
        print("Guild not found")
        return
    
    try:
        member = await guild.fetch_member(user_id)
        await member.edit(mute=should_mute, reason=reason)
        if should_mute:
            print(f'Muted {member.name} for reason: {reason}')
        else:
            print(f'Unmuted {member.name} for reason: {reason}')
    except discord.NotFound:
        print(f'Member with ID {user_id} not found in guild.')
    except discord.Forbidden:
        print('I do not have permission to mute/unmute this user.')
    except discord.HTTPException as e:
        print(f'Failed to mute/unmute user due to HTTP error: {e}')

@app.route('/ping', methods=['GET'])
def ping_pong():
    print('Pong')  # This will log 'Pong' in your console
    return 'Pong', 200

def run_bot():
    client.run(token)

if __name__ == '__main__':
    from threading import Thread
    Thread(target=run_bot).start()
    app.run(port=5000)  # Flask application
