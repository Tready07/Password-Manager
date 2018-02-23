﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Networking.Response;
using Networking;
using System.Windows.Forms;

namespace Password_Manager
{
    class MessageHandler
    {
        Func<MemoryStream, bool>[] functions = { handleApplications };
        public MessageHandler()
        {

        }

        public bool handleMessage(MemoryStream message, MessageHeader header)
        {
            bool isComplete = false;
            try
            {
                isComplete = functions[header.ID](message);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            return isComplete;
        }

        private static bool handleApplications(MemoryStream message)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            ApplicationsResponse resp = (ApplicationsResponse)formatter.Deserialize(message);
            Program.passwordForm.populateTree(resp.applications);
            return true;
        }

        private static bool handleNewApp(MemoryStream message)
        {
            //TODO: handleNewAPPresponse (update applications tree)
            return false;
        }
    }
}