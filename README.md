BashBot
=======

Simple C# bot classes for Toribash.



If you wish to create a bot using this base, you are free to do so.

There are two ways to go about creating your own custom bot using this code:

Events
------
You can subscribe to various events the bot has to offer, these are the CommandRecieved, ChatRecieved and BoutListUpdate events.

    Bot = new Bot(login.Username, login.Password, login.Room.IP, login.Room.Port);
    Bot.ChatRecieved += BotOnChatRecieved;
    Bot.BoutListUpdate += BotOnBoutListUpdate;
    
    private void BotOnChatRecieved(BashCommand line)
    {
        LbMain.Items.Add(BashColourStripper.Strip(line.Value));
    }
    
    private void BotOnBoutListUpdate(IEnumerable<BashBout> bouts)
    {
        LbUsers.ItemsSource = bouts;
    }
    
Modifying the Source
--------------------
If you want better control, you're better off just modifying the source directly!


License
-------
This program is free software: you can redistribute it and/or modify
it under the terms of the GNU General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

See http://www.gnu.org/licenses/ for the full license.
