namespace Ren.CMS.DataWarehousing
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Sql;
    using System.Data.SqlClient;
    using System.Data.SqlTypes;
    using System.Linq;
    using System.Web;

    using Ren.CMS.CORE.SqlHelper;

    public class DataWarehousing
    {
        #region Nested Types

        public partial class ColumnModel
        {
            #region Properties

            public bool AllowNull
            {
                get;
                set;
            }

            public SqlDbType ColumnType
            {
                get;
                set;
            }

            public object DefaultValue
            {
                get;
                set;
            }

            public int IDstart
            {
                get;
                set;
            }

            public int IDStepLength
            {
                get;
                set;
            }

            public bool isIdentity
            {
                get;
                set;
            }

            public long Length
            {
                get;
                set;
            }

            #endregion Properties
        }

        public partial class TableDataModel
        {
            #region Constructors

            public TableDataModel(string tableName)
            {
                TableModel Model = new TableModel();
                Ren.CMS.CORE.ThisApplication.ThisApplication TA = new Ren.CMS.CORE.ThisApplication.ThisApplication();
                string query = "SELECT IDENT_SEED(@tableName) as IdentityStart,IDENT_INCR(@tableName) as IdentitySteps, "+
                    "COLUMNPROPERTY(OBJECT_ID(@tableName),Col.COLUMN_NAME,'isIdentity') as isIdentity, Col.COLUMN_NAME,Col.IS_NULLABLE, "+
                    "Col.COLUMN_DEFAULT, Col.CHARACTER_MAXIMUM_LENGTH, Col.DATA_TYPE "+
                    "FROM INFORMATION_SCHEMA.COLUMNS AS Col "+
                    "LEFT OUTER JOIN INFORMATION_SCHEMA.CONSTRAINT_COLUMN_USAGE AS Usg ON Col.TABLE_NAME = Usg.TABLE_NAME AND Col.COLUMN_NAME = Usg.COLUMN_NAME "+
                    "LEFT OUTER JOIN INFORMATION_SCHEMA.TABLE_CONSTRAINTS AS Con ON Usg.CONSTRAINT_NAME = Con.CONSTRAINT_NAME "+
                    "WHERE Col.TABLE_NAME = @tableName";

                //Hotfix extract dbo.
                string sqlprefix = TA.getSqlPrefix;
                tableName = sqlprefix + tableName;

                nSqlParameterCollection PCol = new nSqlParameterCollection();
                PCol.Add("@tableName", tableName);
                SqlHelper Sql = new SqlHelper();
                Sql.SysConnect();
                SqlDataReader TableSchema = Sql.SysReader(query, PCol);

                if (!TableSchema.HasRows) {
                TableSchema.Close();
                while(!TableSchema.IsClosed){

                }
                    Sql.SysDisconnect();

                    throw new Exception("Table "+ tableName +" does not exists or ren_cms was unable to load Table Shema");

                }
                else
                {

                    while (TableSchema.Read())
                    {
                        ColumnModel MDL = new ColumnModel();
                        MDL.AllowNull = (TableSchema["IS_NULLABLE"].ToString() == "YES" ? true : false);
                        SqlDbType Type = (SqlDbType)Enum.Parse(typeof(SqlDbType), TableSchema["DATA_TYPE"].ToString(), true);
                        MDL.ColumnType = Type;
                        MDL.DefaultValue = TableSchema["COLUMN_DEFAULT"].ToString();
                        MDL.isIdentity = (TableSchema["isIdentity"].ToString() == "1" ? true : false);
                        if (MDL.isIdentity == true)
                        {
                            MDL.IDstart = Convert.ToInt32(TableSchema["IdentityStart"].ToString());
                            MDL.IDStepLength = Convert.ToInt32(TableSchema["IdentitySteps"].ToString());

                        }
                        long len = 0;
                        long.TryParse(TableSchema["CHARACTER_MAXIMUM_LENGTH"].ToString(), out len);

                        MDL.Length = len;

                        Model.Columns.Add(MDL);
                    }
                    TableSchema.Close();
                }
                this.Table = Model;
            }

            #endregion Constructors

            #region Properties

            public TableModel Table
            {
                get; set;
            }

            #endregion Properties
        }

        public partial class TableModel
        {
            #region Fields

            public List<ColumnModel> Columns = new List<ColumnModel>();

            #endregion Fields

            #region Properties

            public string TableName
            {
                get;set;
            }

            #endregion Properties
        }

        #endregion Nested Types
    }
}