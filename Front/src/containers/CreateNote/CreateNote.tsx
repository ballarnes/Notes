import { observer } from 'mobx-react';
import React from 'react'
import { Button, Col, Container, FloatingLabel, Form, Row, Spinner } from 'react-bootstrap'
import { useInjection } from '../../ioc/ioc.react';
import ownTypes from '../../ioc/ownTypes';
import CreateNoteStore from '../../stores/CreateNoteStore';
import { useTranslation } from 'react-i18next';

const CreateNote = observer(() => {
  const store = useInjection<CreateNoteStore>(ownTypes.createNoteStore);
  const { t } = useTranslation();

  return (
    <Container>
      <Row className="justify-content-center">
        <Col lg={4} md={6} xs>
          <Form onSubmit={(ev)=>{ ev.preventDefault();
                                  store.create();
                                }}>
            <FloatingLabel
              controlId="floatingInput"
              label={t('placeholder.title')}
              className="mb-3"
            >
              <Form.Control
              type="text"
              placeholder={t('placeholder.title')}
              value={store.title}
              onChange={(ev)=> {store.changeTitle(ev.target.value)}} />
            </FloatingLabel>
            <FloatingLabel
              controlId="floatingTextarea"
              label={t('placeholder.description')}
              className="mb-3">
              <Form.Control
                as="textarea"
                placeholder={t('placeholder.description')}
                style={{ height: '100px' }}
                onChange={(ev)=> {store.changeDescription(ev.target.value)}} />
            </FloatingLabel>
            <FloatingLabel
              controlId="floatingInput"
              label={t('placeholder.author')}
              className="mb-3"
            >
              <Form.Control
              type="author"
              placeholder={t('placeholder.author')}
              value={store.author}
              onChange={(ev)=> {store.changeAuthor(ev.target.value)}} />
            </FloatingLabel>
            {!!store.error && (
              <p style={{ color: 'red', fontSize: 14 }}>{store.error}</p>
            )}
            <div className="d-grid gap-2">
            <Button variant="outline-success" type="submit">
              {store.isLoading ? (
                <Spinner animation="border" size="sm" />
              ) : (
                `${t('submit')}`
              )}
            </Button>
            </div>
            {!!store.id && (
              <p className="mt-3 mb-3" style={{ color: 'green', fontSize: 14, fontWeight: 700 }}>{t('success', { id: store.id } )}</p>
            )}
          </Form>
        </Col>
      </Row>
    </Container>
  )
});

export default CreateNote
