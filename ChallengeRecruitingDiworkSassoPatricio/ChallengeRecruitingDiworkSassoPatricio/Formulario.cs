using Modelos;
using Presentacion;
using Logica;
using System.Linq;
using System.Text.RegularExpressions;

namespace ChallengeRecruitingDiworkSassoPatricio
{
    public partial class Formulario : Form
    {
        private static Formulario instancia;
        // Para que funcione en caso de trabajar con Threads.
        private static readonly object _bloqueo = new object();

        public static Formulario GetInstance()
        {
            if (instancia == null)
            {
                lock (_bloqueo)
                {
                    if (instancia == null)
                    {
                        instancia = new Formulario();
                    }
                }
            }
            return instancia;
        }

        private Formulario()
        {
            InitializeComponent();
        }

        public List<Modelos.Vehiculo> Vehiculos { get; set; }
        public Modelos.Cliente Cliente { get; set; }
        public List<Modelos.Desperfecto> Desperfectos { get; set; }
        public List<Modelos.Repuesto> Repuestos { get; set; }

        #region Eventos

        private void Form1_Load(object sender, EventArgs e)
        {
            Inicializar();
        }

        #region RadioButton

        private void rbtn_Automovil_CheckedChanged(object sender, EventArgs e)
        {
            if(rbtn_Automovil.Checked)
            {
                lbl_Tipo.Show();
                cmb_Tipo.Show();
                lbl_CantPuertas.Show();
                txt_CantPuertas.Show();
            }
            else
            {
                lbl_Tipo.Hide();
                cmb_Tipo.Hide();
                lbl_CantPuertas.Hide();
                txt_CantPuertas.Hide();
            }
        }

        private void rbtn_Moto_CheckedChanged(object sender, EventArgs e)
        {
            if(rbtn_Moto.Checked)
            {
                lbl_Cilindrada.Visible = true;
                txt_Cilindrada.Visible = true;
            }
            else
            {
                lbl_Cilindrada.Visible = false;
                txt_Cilindrada.Visible = false;
            }
        }

        #endregion

        #region TextBox

        private void txt_CantPuertas_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (Regex.IsMatch(txt_CantPuertas.Text, "[^0-9]"))
                {
                    txt_CantPuertas.Text = txt_CantPuertas.Text.Remove(txt_CantPuertas.Text.Length - 1);
                }
            }
            catch (Exception ex) { }
        }

        #endregion

        #region DataGrivView

        private void dgv_VehiculosIngresados_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dgv_VehiculosIngresados.SelectedRows.Count > 0)
                {
                    var selected = Vehiculos.First(v => v.Patente == ((VehiculoView)dgv_VehiculosIngresados.SelectedRows[0].DataBoundItem).Patente);
                    txt_Patente.Text = selected.Patente;
                    txt_Marca.Text = selected.Marca;
                    txt_Modelo.Text = selected.Modelo;
                    if (selected.GetType().Name == typeof(Modelos.Automovil).Name)
                    {
                        rbtn_Automovil.Checked = true;
                        cmb_Tipo.SelectedIndex = (short)((Modelos.Automovil)selected).TipoAutomovil - 1;
                        txt_CantPuertas.Text = ((Modelos.Automovil)selected).CantidadPuertas.ToString();
                    }
                    else
                    {
                        rbtn_Moto.Checked = true;
                        txt_Cilindrada.Text = ((Modelos.Moto)selected).Cilindrada;
                    }
                    recargarDataGridView(dgv_DesperfectosVehiculo, selected.Desperfectos, new int[] { 0 });
                    InicializarDGVDesperfectos(dgv_DesperfectosVehiculo);
                }
            }
            catch (Exception ex) { }
        }

        private void dgv_DesperfectosVehiculo_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dgv_DesperfectosVehiculo.SelectedRows.Count > 0)
                {
                    var selected = Desperfectos.First(d => d.Id == ((Modelos.Desperfecto)dgv_DesperfectosVehiculo.SelectedRows[0].DataBoundItem).Id);
                    recargarDataGridView(dgv_RepuestosDesperfecto, selected.Repuestos, new int[] { 0 });
                }
            }
            catch (Exception ex) { }
        }

        #endregion

        #region Button

        private void btn_AltaVehiculo_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidarVehiculo())
                {
                    AltaVehiculo();
                    recargarDataGridView(dgv_VehiculosIngresados, Vehiculos.Select(v => new VehiculoView(v)).ToList());
                }
            }
            catch (Exception ex) { }
        }

        private void btn_ModificarVehiculo_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidarVehiculo())
                {
                    BajaVehiculo();
                    AltaVehiculo();
                    recargarDataGridView(dgv_VehiculosIngresados, Vehiculos.Select(v => new VehiculoView(v)).ToList());
                }
            }
            catch (Exception ex) { }

        }

        private void btn_BajaVehiculo_Click(object sender, EventArgs e)
        {
            try
            {
                BajaVehiculo();
                recargarDataGridView(dgv_VehiculosIngresados, Vehiculos.Select(v => new VehiculoView(v)).ToList());
            }
            catch (Exception ex) { }
        }

        private void btn_AgregarDesperfecto_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgv_VehiculosIngresados.SelectedRows.Count > 0)
                {
                    var selectedVehiculo = Vehiculos.First(v => v.Patente == ((VehiculoView)dgv_VehiculosIngresados.SelectedRows[0].DataBoundItem).Patente);
                    AgregarDesperfecto(selectedVehiculo);
                    recargarDataGridView(dgv_DesperfectosVehiculo, selectedVehiculo.Desperfectos.Select(v => new Modelos.Desperfecto(v.Id, v.Descripcion, v.CostoManoObra, v.TiempoTrabajoEstimado)).ToList(), new int[] { 0 });
                    InicializarDGVDesperfectos(dgv_DesperfectosVehiculo);
                }
            }
            catch (Exception ex) { }
        }

        private void btn_QuitarDesperfecto_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgv_VehiculosIngresados.SelectedRows.Count > 0)
                {
                    var selectedVehiculo = Vehiculos.First(v => v.Patente == ((VehiculoView)dgv_VehiculosIngresados.SelectedRows[0].DataBoundItem).Patente);
                    QuitarDesperfecto(selectedVehiculo);
                    recargarDataGridView(dgv_DesperfectosVehiculo, selectedVehiculo.Desperfectos.Select(v => new Modelos.Desperfecto(v.Id, v.Descripcion, v.CostoManoObra, v.TiempoTrabajoEstimado)).ToList(), new int[] { 0 });
                    InicializarDGVDesperfectos(dgv_DesperfectosVehiculo);
                }
            }
            catch (Exception ex) { }
        }

        private void btn_AgregarRepuesto_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgv_DesperfectosVehiculo.SelectedRows.Count > 0)
                {
                    var selectedDesperfecto = Desperfectos.First(d => d.Id == ((Modelos.Desperfecto)dgv_DesperfectosVehiculo.SelectedRows[0].DataBoundItem).Id);
                    AgregarRepuesto(selectedDesperfecto);
                    recargarDataGridView(dgv_RepuestosDesperfecto, selectedDesperfecto.Repuestos.Select(r => new Modelos.Repuesto(id: r.Id, nombre: r.Nombre, precio: r.Precio)).ToList(), new int[] { 0 });
                    InicializarDGVDesperfectos(dgv_Desperfectos);
                }
            }
            catch (Exception ex) { }
        }

        private void btn_QuitarRepuesto_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgv_DesperfectosVehiculo.SelectedRows.Count > 0)
                {
                    var selectedDesperfecto = Desperfectos.First(d => d.Id == ((Modelos.Desperfecto)dgv_DesperfectosVehiculo.SelectedRows[0].DataBoundItem).Id);
                    QuitarRepuesto(selectedDesperfecto);
                    recargarDataGridView(dgv_RepuestosDesperfecto, selectedDesperfecto.Repuestos.Select(r => new Modelos.Repuesto(id: r.Id, nombre: r.Nombre, precio: r.Precio)).ToList(), new int[] { 0 });
                }
            }
            catch (Exception ex) { }
        }

        private void btn_EmitirPresupuesto_Click(object sender, EventArgs e)
        {
            try
            {
                EmitirPresupuesto();
            }
            catch (Exception ex) { }
        }

        private void btnPresupuestos_Click(object sender, EventArgs e)
        {
            try
            {
                InicializarGbxPresupuesto();
            }
            catch (Exception ex) { }
        }

        private void btn_Volver_Click(object sender, EventArgs e)
        {
            try
            {
                Inicializar();
            }
            catch (Exception ex) { }
        }

        private void btn_RepuestoMasUtilizado_Click(object sender, EventArgs e)
        {
            try
            {
                txt_RepuestoMasUtilizado.Text = Logica.Repuesto.ObtenerRepuestoMasUtilizado(txt_MarcaInfoExtra.Text, txt_ModeloInfoExtra.Text);
            }
            catch (Exception ex) { }

        }

        private void btn_PresupuestoPromedio_Click(object sender, EventArgs e)
        {
            try
            {
                txt_PresupuestoPromedio.Text = Logica.Presupuesto.ObtenerPresupuestoPromedioPorMarcaOModelo(txt_MarcaInfoExtra.Text, txt_ModeloInfoExtra.Text);
            }
            catch (Exception ex) { }
            
        }

        private void btn_PresupuestoSumatoriaAutosYMotos_Click(object sender, EventArgs e)
        {
            try
            {
                txt_PresupuestoSumatoriaAutosYMotos.Text = Logica.Presupuesto.ObtenerPresupuestoSumatoriaAutosYMotos();
            }
            catch (Exception ex) { }
        }

        #endregion

        #endregion

        #region Métodos

        #region Utilidades

        public void Inicializar()
        {
            gbx_Presupuestos.Hide();
            Vehiculos = new List<Modelos.Vehiculo>();
            Desperfectos = Logica.Desperfecto.ObtenerTodos();
            Repuestos = Logica.Repuesto.ObtenerTodos();
            rbtn_Automovil.Checked = true;
            cmb_Tipo.DataSource = Enum.GetValues(typeof(Enumerables.TipoAutomovil)).Cast<Enumerables.TipoAutomovil>().ToList();
            recargarDataGridView(dgv_Desperfectos, Desperfectos.Select(d => new Modelos.Desperfecto(d.Id, d.Descripcion, d.CostoManoObra, d.TiempoTrabajoEstimado)).ToList(), new int[] { 0 });
            InicializarDGVDesperfectos(dgv_Desperfectos);
            recargarDataGridView(dgv_Repuestos, Repuestos.Select(r => new Modelos.Repuesto(id : r.Id, nombre : r.Nombre, precio : r.Precio )).ToList(), new int[] { 0 });
            dgv_VehiculosIngresados.DataSource = null;
            dgv_DesperfectosVehiculo.DataSource = null;
            dgv_Presupuestos.DataSource = null;
        }

        public void InicializarDGVDesperfectos(DataGridView dgv)
        {
            dgv.Columns[1].Width = 165;
            dgv.Columns[2].HeaderText = "Mano de Obra";
            dgv.Columns[3].HeaderText = "Días estimados";
        }

        private void InicializarGbxPresupuesto()
        {
            gbx_Presupuestos.Show();
            recargarDataGridView(dgv_Presupuestos, Logica.Presupuesto.ObtenerTodos().Select(p => new { Total = p.Total, Cliente = p.Cliente.Nombre + " " + p.Cliente.Apellido, Vehiculo = p.Vehiculo.Patente }).ToList());
            dgv_Presupuestos.Columns[1].Width = 200;
        }

        private void recargarDataGridView<T>(DataGridView dgv, IEnumerable<T> enumerable, int[]? esconderColumnas = null)
        {
            dgv.DataSource = null;
            dgv.DataSource = enumerable;

            if(esconderColumnas != null)
            {
                foreach (var index in esconderColumnas)
                {
                    dgv.Columns[index].Visible = false;
                }
            }
        }

        #endregion

        private bool ValidarVehiculo()
        {
            bool rta = true;
            if (!Regex.IsMatch(txt_Patente.Text, @"(^[a-z,A-Z]{2}[\d]{3}[a-z,A-Z]{2}$|^[a-z,A-Z]{3}[\d]{3})$"))
            {
                rta = false;
                MessageBox.Show("La patente ingresada es inválida");
            }

            return rta;
        }

        private bool ValidarCliente()
        {
            bool rta = true;
            if (!Regex.IsMatch(txt_Email.Text, "^[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?$"))
            {
                rta = false;
                MessageBox.Show("El email del cliente es inválido");
            }

            return rta;
        }

        private void AltaVehiculo()
        {
            if (!Vehiculos.Exists(x => x.Patente == txt_Patente.Text.ToUpper()))
            {
                if (rbtn_Automovil.Checked)
                {
                    Vehiculos.Add(new Modelos.Automovil(id: 0,
                                                marca: txt_Marca.Text,
                                                modelo: txt_Modelo.Text,
                                                patente: txt_Patente.Text.ToUpper(),
                                                tipoAutomovil: (Enumerables.TipoAutomovil)cmb_Tipo.SelectedValue,
                                                cantidadPuertas: short.Parse(txt_CantPuertas.Text)));
                }
                else
                {
                    Vehiculos.Add(new Modelos.Moto(id: 0,
                                           marca: txt_Marca.Text,
                                           modelo: txt_Modelo.Text,
                                           patente: txt_Patente.Text.ToUpper(),
                                           cilindrada: txt_Cilindrada.Text));
                }
            }
            else
            {
                MessageBox.Show("La patente ya ha sido ingresada");
            }
        }

        private void BajaVehiculo()
        {
            var vehiculo = Vehiculos.Find(x => x.Patente == txt_Patente.Text.ToUpper());
            if (vehiculo != null)
            {
                Vehiculos.Remove(vehiculo);
            }
            else
            {
                MessageBox.Show("El vehículo no existe");
            }
        }

        private void AgregarDesperfecto(Modelos.Vehiculo selectedVehiculo)
        {
            if(dgv_Desperfectos.SelectedRows.Count > 0)
            {
                var selectedDesperfecto = Desperfectos.First(d => d.Id == ((Modelos.Desperfecto)dgv_Desperfectos.SelectedRows[0].DataBoundItem).Id);
                if (selectedVehiculo != null && selectedDesperfecto != null)
                {
                    Logica.Desperfecto.Agregar(selectedVehiculo, selectedDesperfecto);
                }
                else
                {
                    MessageBox.Show("Debe seleccionar un vehículo y un desperfecto");
                }
            }

        }

        private void QuitarDesperfecto(Modelos.Vehiculo selectedVehiculo)
        {
            if(dgv_Desperfectos.SelectedRows.Count > 0)
            {
                var selectedDesperfecto = Desperfectos.First(d => d.Id == ((Modelos.Desperfecto)dgv_Desperfectos.SelectedRows[0].DataBoundItem).Id);
                if (selectedVehiculo != null && selectedDesperfecto != null)
                {
                    Logica.Desperfecto.Quitar(selectedVehiculo, selectedDesperfecto);
                }
                else
                {
                    MessageBox.Show("Debe seleccionar un vehículo y un desperfecto");
                }
            }
        }

        private void AgregarRepuesto(Modelos.Desperfecto selectedDesperfecto)
        {
            if(dgv_Repuestos.SelectedRows.Count > 0)
            {
                var selectedRepuesto = Repuestos.First(r => r.Id == ((Modelos.Repuesto)dgv_Repuestos.SelectedRows[0].DataBoundItem).Id);
                if (selectedDesperfecto != null && selectedRepuesto != null)
                {
                    Logica.Repuesto.Agregar(selectedDesperfecto, selectedRepuesto);
                }
                else
                {
                    MessageBox.Show("Debe seleccionar un vehículo, un desperfecto y un repuesto");
                }
            }
        }

        private void QuitarRepuesto(Modelos.Desperfecto selectedDesperfecto)
        {
            if(dgv_Repuestos.SelectedRows.Count > 0)
            {
                var selectedRepuesto = Repuestos.First(r => r.Id == ((Modelos.Repuesto)dgv_Repuestos.SelectedRows[0].DataBoundItem).Id);
                if (selectedDesperfecto != null && selectedRepuesto != null)
                {
                    Logica.Repuesto.Quitar(selectedDesperfecto, selectedRepuesto);
                }
                else
                {
                    MessageBox.Show("Debe seleccionar un vehículo, un desperfecto y un repuesto");
                }
            }
        }

        private void EmitirPresupuesto()
        {
            if (ValidarCliente())
            {
                long idCliente = Logica.Cliente.Guardar(txt_Nombre.Text, txt_Apellido.Text, txt_Email.Text);
                if (!Vehiculos.Exists(x => x.Desperfectos.Count < 1))
                {
                    foreach (var vehiculo in Vehiculos)
                    {
                        long idVehiculo = 0;
                        if (vehiculo.GetType().Name == typeof(Modelos.Automovil).Name)
                        {
                            var add = (Modelos.Automovil)vehiculo;
                            idVehiculo = Logica.Automovil.Guardar(add.Marca, add.Modelo, add.Patente, (short)add.TipoAutomovil, add.CantidadPuertas);
                        }
                        else
                        {
                            var add = (Modelos.Moto)vehiculo;
                            idVehiculo = Logica.Moto.Guardar(add.Marca, add.Modelo, add.Patente, add.Cilindrada);
                        }

                        if (idVehiculo != 0)
                        {
                            decimal total = Logica.Presupuesto.Calcular(vehiculo.Desperfectos);
                            long idPresupuesto = Logica.Presupuesto.Guardar(total, idCliente, idVehiculo);
                            foreach (var desperfecto in vehiculo.Desperfectos)
                            {
                                foreach (var repuesto in desperfecto.Repuestos)
                                {
                                    Logica.Presupuesto.GuardarPresupuestoDesperfectoRepuesto(idPresupuesto, desperfecto.Id, repuesto.Id);
                                }
                            }
                        }
                    }
                    InicializarGbxPresupuesto();
                }
                else
                {
                    MessageBox.Show("Uno o más de los vehículos no cuentan con desperfectos, si esto es correcto, quítelos de la selección");
                }

            }
        }

        #endregion
    }
}