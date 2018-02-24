using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Networking.Responses;
using Networking;
using System.Windows.Forms;

namespace Password_Manager
{
    class MessageHandler
    {
        Func<MemoryStream, bool>[] functions = { handleApplications,handleNewApp };
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

            // NOTE: Because the MessageHandler will be running in a separate thread, it is important
            //       to use Form.Invoke and put in GUI-related operations inside the lambda function
            //       so that our calls will run/be invoked on the GUI thread.
            //
            //       Otherwise, WinForm will freak out, since we can't make calls to the GUI in
            //       another thread (we can only do it in the thread that the form were created in,
            //       i.e., the GUI thread).
            Program.loginDialog.Invoke((MethodInvoker)(() =>
            {
                Program.loginDialog.Hide();
                Program.passwordForm.Show();
                Program.passwordForm.populateTree(resp.applications);
            }));
            return true;
        }

        private static bool handleNewApp(MemoryStream message)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            NewAppResponse resp = (NewAppResponse)formatter.Deserialize(message);


            Program.passwordForm.Invoke((MethodInvoker)(() =>
            {
                Program.passwordForm.addAppToTree(resp.application);
            }));          
            return true;
        }
    }
}
