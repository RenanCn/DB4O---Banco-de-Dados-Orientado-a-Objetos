/* This file is part of the db4o object database http://www.db4o.com

Copyright (C) 2004 - 2011  Versant Corporation http://www.versant.com

db4o is free software; you can redistribute it and/or modify it under
the terms of version 3 of the GNU General Public License as published
by the Free Software Foundation.

db4o is distributed in the hope that it will be useful, but WITHOUT ANY
WARRANTY; without even the implied warranty of MERCHANTABILITY or
FITNESS FOR A PARTICULAR PURPOSE.  See the GNU General Public License
for more details.

You should have received a copy of the GNU General Public License along
with this program.  If not, see http://www.gnu.org/licenses/. */
using Db4oUnit.Extensions;
using Db4oUnit.Extensions.Fixtures;
using Db4objects.Db4o.Internal;

namespace Db4objects.Db4o.Tests.Common.Freespace
{
	public abstract class FileSizeTestCaseBase : AbstractDb4oTestCase, IOptOutTA, IOptOutInMemory
	{
		protected virtual int DatabaseFileSize()
		{
			LocalObjectContainer localContainer = Fixture().FileSession();
			localContainer.SyncFiles();
			long length = new Sharpen.IO.File(localContainer.FileName()).Length();
			return (int)length;
		}
	}
}