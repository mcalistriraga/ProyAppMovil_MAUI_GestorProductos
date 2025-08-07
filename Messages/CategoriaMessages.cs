using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.Messaging.Messages;

namespace MauiAppGestorMovil.Messages
{
    public class CategoriaEditadaMessage : ValueChangedMessage<bool>
    {
        public CategoriaEditadaMessage(bool value) : base(value) { }
    }

    public class RecargarCategoriasMessage : ValueChangedMessage<bool>
    {
        public RecargarCategoriasMessage(bool value) : base(value) { }
    }

    public class RecargarProductosMessage : ValueChangedMessage<bool>
    {
        public RecargarProductosMessage(bool value) : base(value) { }
    }
}
