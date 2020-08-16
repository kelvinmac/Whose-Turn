package Security

// Represents a single has which will be stored for a given user
type StoredHash struct {
	Memory      uint32
	Iterations  uint32
	Parallelism uint8
	Hash        string
	Salt        string
	KeyLength   uint32
}
