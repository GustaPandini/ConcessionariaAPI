import { useState, useEffect } from 'react';
import axios from 'axios';
import { FaTrash, FaPencilAlt, FaPlus, FaTimes } from 'react-icons/fa';

const API_URL = 'https://localhost:7112/api/Automovel'; 

function App() {
  const [automoveis, setAutomoveis] = useState([]);
  const [erro, setErro] = useState('');

  const [isModalAberto, setIsModalAberto] = useState(false);
  const [modoEdicao, setModoEdicao] = useState(false);
  const [idEmEdicao, setIdEmEdicao] = useState(null);

  const [formData, setFormData] = useState({
    marca: '',
    modelo: '',
    versao: '',
    cor: '',
    ano: new Date().getFullYear(),
    anoModelo: new Date().getFullYear(),
    quilometragem: null,
    powertrain: '',
    blindado: false,
    preco: null,
    quantidadeDonos: null
  });

  useEffect(() => {
    buscarAutomoveis();
  }, []);

  const buscarAutomoveis = async () => {
    try {
      const response = await axios.get(API_URL);
      setAutomoveis(response.data);
    } catch (err) {
      setErro('Erro ao carregar os automóveis.');
    }
  };

  const abrirModalCadastro = () => {
    setModoEdicao(false);
    setIdEmEdicao(null);
    setFormData({
      marca: '', modelo: '', versao: '', cor: '',
      ano: null, anoModelo: null, quilometragem: null,
      powertrain: '', blindado: false, preco: null, quantidadeDonos: null
    });
    setIsModalAberto(true);
  };

  const abrirModalEdicao = (carro) => {
    setModoEdicao(true);
    setIdEmEdicao(carro.id);
    setFormData({ ...carro });
    setIsModalAberto(true);
  };

  const handleInputChange = (e) => {
    const { name, value, type, checked } = e.target;
    setFormData({
      ...formData,
      [name]: type === 'checkbox' ? checked : (type === 'number' ? Number(value) : value)
    });
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    try {
      if (modoEdicao) {
        await axios.put(`${API_URL}/${idEmEdicao}`, formData);
      } else {
        await axios.post(API_URL, formData);
      }
      setIsModalAberto(false);
      buscarAutomoveis();
    } catch (err) {
      alert('Erro ao salvar os dados do automóvel.');
    }
  };

  const handleDeletar = async (id) => {
    if (window.confirm('Deseja realmente deletar este veículo?')) {
      try {
        await axios.delete(`${API_URL}/${id}`);
        buscarAutomoveis();
      } catch (err) {
        alert('Erro ao deletar.');
      }
    }
  };

  return (
    <div style={styles.container}>
      
      <header style={styles.header}>
        <div style={styles.logo}>Tainy</div>
        <button onClick={abrirModalCadastro} style={styles.addButton}>
          <FaPlus style={{ marginRight: '8px' }} /> Adicionar Automóvel
        </button>
      </header>

      {erro && <div style={styles.errorBanner}>{erro}</div>}

      <main style={styles.mainContent}>
        <div style={styles.grid}>
          {automoveis.map((carro) => (
            <div key={carro.id} style={styles.card}>
              <div style={styles.cardInfo}>
                <h3 style={styles.carTitle}>{carro.marca} {carro.modelo}</h3>
                <p style={styles.carDetails}>{carro.versao} • {carro.cor} {carro.blindado && '🛡️'}</p>
                <p style={styles.carSpecs}>{carro.ano}/{carro.anoModelo} • {carro.powertrain} • {carro.quilometragem.toLocaleString()} km</p>
                <div style={styles.priceTag}>
                  {carro.preco.toLocaleString('pt-BR', { style: 'currency', currency: 'BRL' })}
                </div>
              </div>

              <div style={styles.cardActions}>
                <button 
                  onClick={() => abrirModalEdicao(carro)} 
                  style={{ ...styles.actionButton, backgroundColor: '#f1f3f5', color: '#495057' }}
                  title="Editar"
                >
                  <FaPencilAlt />
                </button>
                <button 
                  onClick={() => handleDeletar(carro.id)} 
                  style={{ ...styles.actionButton, backgroundColor: '#fff5f5', color: '#e03131' }}
                  title="Deletar"
                >
                  <FaTrash />
                </button>
              </div>
            </div>
          ))}
        </div>
      </main>

      {isModalAberto && (
        <div style={styles.modalOverlay}>
          <div style={styles.modalContent}>
            
            <div style={styles.modalHeader}>
              <h2>{modoEdicao ? 'Editar Automóvel' : 'Cadastrar Novo Automóvel'}</h2>
              <button onClick={() => setIsModalAberto(false)} style={styles.closeButton}>
                <FaTimes />
              </button>
            </div>

            <form onSubmit={handleSubmit} style={styles.form}>
              <div style={styles.formRow}>
                <label style={styles.label}>Marca:
                  <input type="text" name="marca" value={formData.marca} onChange={handleInputChange} required style={styles.input} />
                </label>
                <label style={styles.label}>Modelo:
                  <input type="text" name="modelo" value={formData.modelo} onChange={handleInputChange} required style={styles.input} />
                </label>
              </div>

              <div style={styles.formRow}>
                <label style={styles.label}>Versão:
                  <input type="text" name="versao" value={formData.versao} onChange={handleInputChange} required style={styles.input} />
                </label>
                <label style={styles.label}>Cor:
                  <input type="text" name="cor" value={formData.cor} onChange={handleInputChange} required style={styles.input} />
                </label>
              </div>

              <div style={styles.formRow}>
                <label style={styles.label}>Ano Fabricação:
                  <input type="number" name="ano" value={formData.ano} onChange={handleInputChange} required style={styles.input} />
                </label>
                <label style={styles.label}>Ano Modelo:
                  <input type="number" name="anoModelo" value={formData.anoModelo} onChange={handleInputChange} required style={styles.input} />
                </label>
              </div>

              <div style={styles.formRow}>
                <label style={styles.label}>KM:
                  <input type="number" name="quilometragem" value={formData.quilometragem} onChange={handleInputChange} required style={styles.input} />
                </label>
                <label style={styles.label}>PowerTrain:
                  <input name="powertrain" value={formData.powertrain} onChange={handleInputChange} style={styles.input}></input>
                </label>
              </div>

              <div style={styles.formRow}>
                <label style={styles.label}>Preço (R$):
                  <input type="number" name="preco" value={formData.preco} onChange={handleInputChange} required style={styles.input} />
                </label>
                <label style={styles.label}>Nº de Donos:
                  <input type="number" name="quantidadeDonos" value={formData.quantidadeDonos} onChange={handleInputChange} required style={styles.input} />
                </label>
              </div>

              <div style={styles.checkboxRow}>
                <label style={styles.checkboxLabel}>
                  <input type="checkbox" name="blindado" checked={formData.blindado} onChange={handleInputChange} />
                  Veículo Blindado
                </label>
              </div>

              <button type="submit" style={styles.saveButton}>
                {modoEdicao ? 'Salvar Alterações' : 'Concluir Cadastro'}
              </button>
            </form>

          </div>
        </div>
      )}

    </div>
  );
}

const styles = {
  container: { fontFamily: '"Segoe UI", Roboto, sans-serif', backgroundColor: '#f8f9fa', minHeight: '100vh', paddingTop: '90px', color: '#212529' },
  header: { position: 'fixed', top: 0, left: 0, right: 0, height: '70px', backgroundColor: '#1a1b1e', display: 'flex', justifyContent: 'space-between', alignItems: 'center', padding: '0 40px', boxShadow: '0 4px 10px rgba(0,0,0,0.1)', zIndex: 1000 },
  logo: { color: '#ffffff', fontSize: '1.6rem', fontWeight: '800', letterSpacing: '-1px', fontStyle: 'italic' },
  addButton: { backgroundColor: '#228be6', color: 'white', border: 'none', padding: '10px 20px', borderRadius: '8px', fontSize: '0.95rem', fontWeight: '600', cursor: 'pointer', display: 'flex', alignItems: 'center' },
  errorBanner: { backgroundColor: '#ffe3e3', color: '#e03131', padding: '15px', textAlign: 'center', fontWeight: 'bold', maxWidth: '1200px', margin: '10px auto', borderRadius: '8px' },
  mainContent: { maxWidth: '1200px', margin: '0 auto', padding: '20px' },
  grid: { display: 'grid', gridTemplateColumns: 'repeat(auto-fill, minmax(280px, 1fr))', gap: '24px' },
  card: { backgroundColor: '#ffffff', borderRadius: '12px', border: '1px solid #dee2e6', boxShadow: '0 4px 6px rgba(0,0,0,0.05)', display: 'flex', flexDirection: 'column' },
  cardInfo: { padding: '20px', flexGrow: 1 },
  carTitle: { fontSize: '1.4rem', margin: '0 0 8px 0', fontWeight: '700', textTransform: 'uppercase' },
  carDetails: { margin: '0 0 4px 0', color: '#495057', fontSize: '0.95rem' },
  carSpecs: { margin: '0 0 16px 0', color: '#868e96', fontSize: '0.85rem' },
  priceTag: { fontSize: '1.3rem', fontWeight: '700', color: '#2b8a3e' },
  cardActions: { display: 'flex', borderTop: '1px solid #f1f3f5', padding: '12px 20px', gap: '10px', backgroundColor: '#fafbfc' },
  actionButton: { flex: 1, padding: '10px', border: 'none', borderRadius: '6px', cursor: 'pointer', display: 'flex', alignItems: 'center', justifyContent: 'center' },
  
  modalOverlay: {
    position: 'fixed',
    top: 0, left: 0, right: 0, bottom: 0,
    backgroundColor: 'rgba(0, 0, 0, 0.6)',
    display: 'flex', justifyContent: 'center', alignItems: 'center',
    zIndex: 2000
  },
  modalContent: {
    backgroundColor: '#ffffff',
    borderRadius: '12px',
    width: '90%', maxWidth: '550px',
    padding: '30px',
    boxShadow: '0 10px 25px rgba(0,0,0,0.2)',
    animation: 'fadeIn 0.3s ease'
  },
  modalHeader: { display: 'flex', justifyContent: 'space-between', alignItems: 'center', borderBottom: '1px solid #dee2e6', paddingBottom: '15px', marginBottom: '20px' },
  closeButton: { background: 'none', border: 'none', fontSize: '1.2rem', cursor: 'pointer', color: '#868e96' },
  form: { display: 'flex', flexDirection: 'column', gap: '16px' },
  formRow: { display: 'flex', gap: '16px' },
  label: { display: 'flex', flexDirection: 'column', flex: 1, fontSize: '0.9rem', fontWeight: '600', color: '#495057' },
  input: { marginTop: '6px', padding: '10px', borderRadius: '6px', border: '1px solid #ced4da', fontSize: '0.95rem' },
  checkboxRow: { display: 'flex', alignItems: 'center', margin: '5px 0' },
  checkboxLabel: { display: 'flex', alignItems: 'center', gap: '8px', fontSize: '0.95rem', cursor: 'pointer' },
  saveButton: { backgroundColor: '#2b8a3e', color: 'white', border: 'none', padding: '12px', borderRadius: '8px', fontSize: '1rem', fontWeight: '600', cursor: 'pointer', marginTop: '10px' }
};

export default App;