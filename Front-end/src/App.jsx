import { useState, useEffect } from 'react';
import axios from 'axios';
import { FaTrash, FaPencilAlt, FaPlus } from 'react-icons/fa';

const API_URL = 'https://localhost:7123/api/Automovel'; 

function App() {
  const [automoveis, setAutomoveis] = useState([]);
  const [erro, setErro] = useState('');

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
      
      {/* CABEÇALHO (FIXO NO TOPO) */}
      <header style={styles.header}>
        <div style={styles.logo}>Tainy</div>
        <button onClick={() => alert('Abrir modal de cadastro')} style={styles.addButton}>
          <FaPlus style={{ marginRight: '8px' }} /> Adicionar Automóvel
        </button>
      </header>

      {/* MENSAGEM DE ERRO CASO A API FALHE */}
      {erro && <div style={styles.errorBanner}>{erro}</div>}

      {/* BODY / GRID DE CARTÕES */}
      <main style={styles.mainContent}>
        <div style={styles.grid}>
          {automoveis.map((carro) => (
            <div key={carro.id} style={styles.card}>
              
              {/* Informações do Carro */}
              <div style={styles.cardInfo}>
                <h3 style={styles.carTitle}>{carro.marca} {carro.modelo}</h3>
                <p style={styles.carDetails}>{carro.versao} • {carro.cor} {carro.blindado && '🛡️'}</p>
                <p style={styles.carSpecs}>{carro.ano}/{carro.anoModelo} • {carro.powertrain} • {carro.quilometragem.toLocaleString()} km</p>
                <div style={styles.priceTag}>
                  {carro.preco.toLocaleString('pt-BR', { style: 'currency', currency: 'BRL' })}
                </div>
              </div>

              {/* Botões de Ação no Rodapé do Cartão */}
              <div style={styles.cardActions}>
                <button 
                  onClick={() => alert('Função Editar em desenvolvimento')} 
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
    </div>
  );
}

// --- ESTILIZAÇÃO ATUALIZADA ---
const styles = {
  container: {
    fontFamily: '"Segoe UI", Roboto, Helvetica, Arial, sans-serif',
    backgroundColor: '#f8f9fa',
    minHeight: '100vh',
    paddingTop: '90px', // IMPORTANTE: Abre espaço no topo para o cabeçalho não cobrir os cartões
    color: '#212529'
  },
  header: {
    position: 'fixed',
    top: 0, // Movido para o topo
    left: 0,
    right: 0,
    height: '70px',
    backgroundColor: '#1a1b1e', // Mantém o grafite escuro elegante
    display: 'flex',
    justifyContent: 'space-between',
    alignItems: 'center',
    padding: '0 40px',
    boxShadow: '0 4px 10px rgba(0,0,0,0.1)', // Sombra agora aponta para baixo
    zIndex: 1000
  },
  logo: {
    color: '#ffffff',
    fontSize: '1.6rem',
    fontWeight: '800',
    letterSpacing: '-1px',
    fontStyle: 'italic'
  },
  addButton: {
    backgroundColor: '#228be6',
    color: 'white',
    border: 'none',
    padding: '10px 20px',
    borderRadius: '8px',
    fontSize: '0.95rem',
    fontWeight: '600',
    cursor: 'pointer',
    display: 'flex',
    alignItems: 'center',
    boxShadow: '0 2px 4px rgba(34,139,230,0.3)',
    transition: 'background-color 0.2s'
  },
  errorBanner: {
    backgroundColor: '#ffe3e3',
    color: '#e03131',
    padding: '15px',
    textAlign: 'center',
    fontWeight: 'bold',
    maxWidth: '1200px',
    margin: '10px auto 0 auto',
    borderRadius: '8px'
  },
  mainContent: {
    maxWidth: '1200px',
    margin: '0 auto',
    padding: '20px 20px 40px 20px'
  },
  grid: {
    display: 'grid',
    gridTemplateColumns: 'repeat(auto-fill, minmax(280px, 1fr))',
    gap: '24px'
  },
  card: {
    backgroundColor: '#ffffff',
    borderRadius: '12px',
    border: '1px solid #dee2e6',
    boxShadow: '0 4px 6px -1px rgba(0,0,0,0.05), 0 2px 4px -1px rgba(0,0,0,0.03)',
    display: 'flex',
    flexDirection: 'column',
    justifyContent: 'between',
    overflow: 'hidden',
  },
  cardInfo: {
    padding: '20px',
    flexGrow: 1
  },
  carTitle: {
    fontSize: '1.4rem',
    margin: '0 0 8px 0',
    color: '#1a1b1e',
    fontWeight: '700',
    textTransform: 'uppercase',
    letterSpacing: '0.5px'
  },
  carDetails: {
    margin: '0 0 4px 0',
    color: '#495057',
    fontSize: '0.95rem',
    fontWeight: '500'
  },
  carSpecs: {
    margin: '0 0 16px 0',
    color: '#868e96',
    fontSize: '0.85rem'
  },
  priceTag: {
    fontSize: '1.3rem',
    fontWeight: '700',
    color: '#2b8a3e'
  },
  cardActions: {
    display: 'flex',
    borderTop: '1px solid #f1f3f5',
    padding: '12px 20px',
    gap: '10px',
    backgroundColor: '#fafbfc'
  },
  actionButton: {
    flex: 1,
    padding: '10px',
    border: 'none',
    borderRadius: '6px',
    cursor: 'pointer',
    fontSize: '1rem',
    display: 'flex',
    alignItems: 'center',
    justifyContent: 'center'
  }
};

export default App;